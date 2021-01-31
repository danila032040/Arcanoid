using System.Collections.Generic;
using Context;
using Scenes.Game.Balls.Base;
using Scenes.Game.Contexts;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private void Awake()
        {
            _gameContext.HpController.HealthValueChanged += OnHealthValueChanged;
            _gameContext.BallsManager.BallsChanged += BallsManagerOnBallsChanged;
            _gameContext.OutOfBoundsWall.OutOfBounds += OutOfBoundsWallOnOutOfBounds;
        }

        private void BallsManagerOnBallsChanged(List<Ball> oldBalls, List<Ball> newBalls)
        {
            _gameContext.GameStatusManager.ChangeBallsSpeedOnBlocksCount();
        }

        public event OnValueChanged<int> HealthValueChanged;

        private void OnHealthValueChanged(int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(oldValue, newValue);
        }

        public void Reset()
        {
            _gameContext.HpController.SetHpValue(ProjectContext.Instance.GetHealthConfig().InitialPlayerHealthValue);
            AttachBall(_gameContext.BallsManager.SpawnBall());
            _gameContext.BallsManager.SetCurrentSpeedProgress(_gameContext.GameStatusManager.GetCurrentProgress());
        }

        private void OutOfBoundsWallOnOutOfBounds(GameObject obj)
        {
            Ball ball = obj.GetComponent<Ball>();
            if (!ReferenceEquals(ball, null))
            {
                _gameContext.BallsManager.RemoveBall(ball);
                if (_gameContext.BallsManager.GetBallsCount() <= 0)
                {
                    AttachBall(_gameContext.BallsManager.SpawnBall());
                    _gameContext.HpController.AddHpValue(ProjectContext.Instance.GetHealthConfig()
                        .AddHealthToPlayerForLoosingAllBalls);
                }
            }
        }

        private Ball _attachedBall;

        private void AttachBall(Ball ball)
        {
            _attachedBall = ball;
            _attachedBall.GetBallAttachment().AttachTo(_gameContext.Paddle.transform);
            _gameContext.InputServicePopUp.MouseButtonUp += DetachBall;
        }

        private void DetachBall()
        {
            _gameContext.InputServicePopUp.MouseButtonUp -= DetachBall;
            _attachedBall.GetBallAttachment().Detach();
            _attachedBall.GetBallMovement().StartMoving();
        }
    }
}