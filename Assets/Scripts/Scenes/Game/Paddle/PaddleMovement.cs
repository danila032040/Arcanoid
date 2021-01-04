namespace Scripts.Scenes.Game.Paddle
{
    using Scripts.Scenes.Game.Input;
    using UnityEngine;

    public class PaddleMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private PaddleView _paddleView;

        private float _goalXPosition;

        private IInputService _inputService;
        private Camera _camera;

        public void Init(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;
        }

        private void Start()
        {
            this.Init(FindObjectOfType<InputService>(), Camera.main);

            _inputService.OnMouseButtonDown += StartMovingPaddle;
            _inputService.OnMouseButtonUp += EndMovingPaddle;
            _inputService.OnMousePositionChanged += ChangeGoalPositionByMouse;
        }
        private void OnDestroy()
        {
            _inputService.OnMouseButtonDown -= StartMovingPaddle;
            _inputService.OnMouseButtonUp -= EndMovingPaddle;
            _inputService.OnMousePositionChanged -= ChangeGoalPositionByMouse;
        }


        private void Update()
        {
            if (_movePaddle)
            {
                MovePaddleToGoalPosition();
            }
        }



        private bool _movePaddle;
        private void StartMovingPaddle()
        {
            _movePaddle = true;
        }
        private void EndMovingPaddle()
        {
            _movePaddle = false;
        }
        private void ChangeGoalPositionByMouse(Vector3 mousePosition)
        {
            SetGoalPosition(_camera.ScreenToWorldPoint(mousePosition).x);
        }


        public void SetGoalPosition(float x)
        {
            float leftClamp = _camera.ViewportToWorldPoint(Vector3.zero).x;
            float rightClamp = _camera.ViewportToWorldPoint(Vector3.right).x;

            leftClamp += _paddleView.Width / 2;
            rightClamp -= _paddleView.Width / 2;

            _goalXPosition = Mathf.Clamp(x, leftClamp, rightClamp);
        }
        private void MovePaddleToGoalPosition()
        {
            float nextXPosition = this.transform.position.x;

            if (nextXPosition != _goalXPosition)
            {

                float moveXDirection = nextXPosition > _goalXPosition ? -1 : +1;

                float moveXDistance = GetWorldPointWidth(_camera) * _moveSpeed * Time.deltaTime;

                nextXPosition = nextXPosition + moveXDirection * moveXDistance;

                if (moveXDirection > 0) nextXPosition = Mathf.Min(nextXPosition, _goalXPosition);
                else nextXPosition = Mathf.Max(nextXPosition, _goalXPosition);

                Vector3 nextPosition = this.transform.position;
                nextPosition.x = nextXPosition;

                this.transform.position = nextPosition;
            }
        }


        private float GetWorldPointWidth(Camera camera)
        {
            return (camera.ViewportToWorldPoint(Vector3.right) - camera.ViewportToWorldPoint(Vector3.zero)).magnitude;
        }
    }
}
