namespace Scripts.Scenes.Game.Managers
{
    using Scripts.Scenes.Game.Balls;
    using Scripts.Scenes.Game.Input;
    using Scripts.Scenes.Game.Paddles;
    using UnityEngine;
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private Ball _startBall;
        [SerializeField] private Paddle _paddle;


        private IInputService _inputService;

        private void Init(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Start()
        {
            Init(FindObjectOfType<InputService>());
            StartGame();
        }

        private void StartGame()
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
