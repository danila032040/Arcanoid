namespace Scripts.Scenes.Game.Managers
{
    using Scripts.SaveLoader;
    using Scripts.SaveLoader.Interfaces;
    using Scripts.Scenes.Game.Balls;
    using Scripts.Scenes.Game.Input;
    using Scripts.Scenes.Game.Paddles;
    using UnityEngine;
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Ball _startBall;
        [SerializeField] private Paddle _paddle;


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
            _levelSaveLoader.SaveLevel(new LevelInfo { name = "Test" });
            AttachBall();
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
