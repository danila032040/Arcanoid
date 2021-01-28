using System;
using System.Collections.Generic;
using Configurations;
using Context;
using Scenes.Game.Balls;
using Scenes.Game.Balls.Base;
using Scenes.Game.Paddles;
using Scenes.Game.Player;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scenes.Game.Utils;
using Scenes.Game.Walls;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private IInputService _inputService;

        [SerializeField] private GameStatusManager _gameStatusManager;

        [SerializeField] private HpController _hpController;
        [SerializeField] private BallsManager _ballsManager;


        [SerializeField] private Paddle _paddle;
        [SerializeField] private OutOfBoundsWall _outOfBoundsWall;


        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }


        [SerializeField] private InputService _inputServiceImpl;

        private void Awake()
        {
            Init(_inputServiceImpl);

            _hpController.HealthValueChanged += OnHealthValueChanged;
            _ballsManager.BallsChanged += BallsManagerOnBallsChanged;
            _outOfBoundsWall.OutOfBounds += OutOfBoundsWallOnOutOfBounds;
        }

        private void BallsManagerOnBallsChanged(List<Ball> balls)
        {
            _gameStatusManager.ChangeBallsSpeedOnBlocksCount();
        }

        public event OnIntValueChanged HealthValueChanged;

        protected virtual void OnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(sender, oldValue, newValue);
        }

        public void Reset()
        {
            _hpController.SetHpValue(ProjectContext.Instance.GetHealthConfig().InitialPlayerHealthValue);
            AttachBall(_ballsManager.SpawnBall());
        }

        private void OutOfBoundsWallOnOutOfBounds(GameObject obj)
        {
            Ball ball = obj.GetComponent<Ball>();
            if (!ReferenceEquals(ball, null))
            {
                _ballsManager.RemoveBall(ball);
                if (_ballsManager.GetBalls().Count <= 0)
                {
                    AttachBall(_ballsManager.SpawnBall());
                    _hpController.AddHpValue(ProjectContext.Instance.GetHealthConfig()
                        .AddHealthToPlayerForLoosingAllBalls);
                }
            }
        }

        private Ball _attachedBall;

        private void AttachBall(Ball ball)
        {
            _attachedBall = ball;
            _attachedBall.GetBallAttachment().AttachTo(_paddle.transform);
            _inputService.MouseButtonUp += DetachBall;
        }

        private void DetachBall()
        {
            _inputService.MouseButtonUp -= DetachBall;
            _attachedBall.GetBallAttachment().Detach();
            _attachedBall.GetBallMovement().StartMoving();
        }
    }
}