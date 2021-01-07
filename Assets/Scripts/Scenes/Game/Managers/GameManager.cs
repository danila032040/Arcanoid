using Scenes.Game.Paddles;

namespace Scripts.Scenes.Game.Managers
{
    using Scripts.SaveLoader;
    using Scripts.SaveLoader.Interfaces;
    using Scripts.Scenes.Game.Balls;
    using Scripts.Scenes.Game.Bricks;
    using Scripts.Scenes.Game.Input;
    using UnityEngine;
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
            LevelInfo info = _levelSaveLoader.LoadLevel("Test");
            _briksManager.SpawnBricks(info.Map, info.BrickHeight, info.LeftOffset, info.RightOffset, info.OffsetBetweenRows, info.OffsetBetweenCols);
        }

        private void AttachBall()
        {

            _startBall.AttachTo(_paddle.gameObject);
            _inputService.OnMouseButtonUp += DetachBall;
        }
        private void DetachBall()
        {
            _inputService.OnMouseButtonUp -= DetachBall;
            _startBall.Detach();
            _startBall.StartMoving();
        }
    }
}
