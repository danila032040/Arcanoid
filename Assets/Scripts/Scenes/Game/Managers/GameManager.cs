using SaveLoader;
using SaveLoader.Interfaces;
using Scenes.Game.Balls;
using Scenes.Game.Paddles;
using Scripts.Scenes.Game.Bricks;
using Scripts.Scenes.Game.Input;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Ball _startBall;
        [SerializeField] private Paddle _paddle;

        [SerializeField] private BriksManager _briksManager;

        private IInputService _inputService;
        private ILevelSaveLoader _levelSaveLoader;

        private void Init(IInputService inputService, ILevelSaveLoader levelSaveLoader)
        {
            _inputService = inputService;
            _levelSaveLoader = levelSaveLoader;
        }

        public void Start()
        {
            Init(FindObjectOfType<InputService>(), new JsonSaveLoader());
            StartGame();
        }

        private void StartGame()
        {
            AttachBall();
            LevelInfo info = _levelSaveLoader.LoadLevel("FirstLevel");
            _briksManager.SpawnBricks(info.Map, info.BrickHeight, info.LeftOffset, info.RightOffset, info.OffsetBetweenRows, info.OffsetBetweenCols);
        }

        private void AttachBall()
        {

            _startBall.GetBallAttachment().AttachTo(_paddle.gameObject);
            _inputService.OnMouseButtonUp += DetachBall;
        }
        private void DetachBall()
        {
            _inputService.OnMouseButtonUp -= DetachBall;
            _startBall.GetBallAttachment().Detach();
            _startBall.GetBallMovement().StartMoving();
        }
    }
}
