using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
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
        private ILevelInfoSaveLoader _levelInfoSaveLoader;

        private void Init(IInputService inputService, ILevelInfoSaveLoader levelInfoSaveLoader)
        {
            _inputService = inputService;
            _levelInfoSaveLoader = levelInfoSaveLoader;
        }

        public void Start()
        {
            Init(FindObjectOfType<InputService>(), new InfoSaveLoader());
            StartGame();
        }

        private void StartGame()
        {
            AttachBall();
            LevelInfo info = _levelInfoSaveLoader.LoadLevelInfo("FirstLevel");
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
