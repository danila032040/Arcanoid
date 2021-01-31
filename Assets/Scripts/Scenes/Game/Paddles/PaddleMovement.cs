using System;
using Scenes.Game.Services.Cameras.Implementations;
using Scenes.Game.Services.Cameras.Interfaces;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using UnityEngine;

namespace Scenes.Game.Paddles
{
    public class PaddleMovement : MonoBehaviour
    {
        [SerializeField] private float _initialMoveSpeed;


        [SerializeField] private PaddleView _paddleView;

        [SerializeField] private Rigidbody2D _rb;

        private float _goalXPosition;
        private float _currentMoveSpeed;

        private IInputService _inputService;
        private ICameraService _cameraService;
        private Camera _camera;

        public void Init(IInputService inputService, ICameraService cameraService, Camera currentCamera)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _camera = currentCamera;
        }

        private void Start()
        {
            this.Init(FindObjectOfType<InputServicePopUp>(), new CameraService(), Camera.main);

            _currentMoveSpeed = _initialMoveSpeed;

            _inputService.MouseButtonDown += StartMovingPaddle;
            _inputService.MouseButtonUp += EndMovingPaddle;
            _inputService.MousePositionChanged += ChangeGoalPositionByMouse;
        }

        private void OnDestroy()
        {
            if (_inputService == null) return;

            _inputService.MouseButtonDown -= StartMovingPaddle;
            _inputService.MouseButtonUp -= EndMovingPaddle;
            _inputService.MousePositionChanged -= ChangeGoalPositionByMouse;
        }

        private void FixedUpdate()
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


        private void SetGoalPosition(float x)
        {
            float leftClamp = _camera.ViewportToWorldPoint(Vector3.zero).x;
            float rightClamp = _camera.ViewportToWorldPoint(Vector3.right).x;

            leftClamp += _paddleView.Width / 2;
            rightClamp -= _paddleView.Width / 2;

            _goalXPosition = Mathf.Clamp(x, leftClamp, rightClamp);
        }

        private void MovePaddleToGoalPosition()
        {
            Vector3 nextPosition = this.transform.position;

            float nextXPosition = nextPosition.x;

            if (Math.Abs(nextXPosition - _goalXPosition) > 1e-2)
            {
                float moveXDirection = nextXPosition > _goalXPosition ? -1 : +1;

                float moveXDistance = _cameraService.GetWorldPointWidth(_camera) * _currentMoveSpeed * Time.deltaTime *
                                      (1 + Mathf.Clamp01(1f - _rb.drag * Time.deltaTime));

                nextXPosition = nextXPosition + moveXDirection * moveXDistance;

                nextXPosition = moveXDirection > 0 ? Mathf.Min(nextXPosition, _goalXPosition) : Mathf.Max(nextXPosition, _goalXPosition);


                nextPosition.x = nextXPosition;

                float distanceToGoal = Mathf.Abs(_goalXPosition - this.transform.position.x);

                Vector2 velocity = new Vector2(
                    moveXDirection * _currentMoveSpeed * _cameraService.GetWorldPointWidth(_camera),
                    0);

                _rb.velocity = Vector2.Lerp(Vector2.zero, velocity,
                    (1f - (velocity + (_rb.velocity * Mathf.Clamp01(1f - _rb.drag * Time.deltaTime)
                        )).magnitude * Time.deltaTime / distanceToGoal));
            }
        }


        public float GetInitialSpeed() => _initialMoveSpeed;
        public float GetCurrentSpeed() => _currentMoveSpeed;
        public void SetSpeed(float value) => _currentMoveSpeed = value;
    }
}