using System;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scripts.Scenes.Game.Camera.Implementations;
using Scripts.Scenes.Game.Camera.Intrefaces;
using UnityEngine;

namespace Scenes.Game.Paddles
{
    public class PaddleMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private PaddleView _paddleView;

        [SerializeField] private Rigidbody2D _rb;

        private float _goalXPosition;

        private IInputService _inputService;
        private ICameraService _cameraService;
        private Camera _camera;

        public void Init(IInputService inputService, ICameraService cameraService, Camera camera)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _camera = camera;
        }

        private void Start()
        {
            this.Init(FindObjectOfType<InputService>(),new CameraService(), Camera.main);

            _inputService.OnMouseButtonDown += StartMovingPaddle;
            _inputService.OnMouseButtonUp += EndMovingPaddle;
            _inputService.OnMousePositionChanged += ChangeGoalPositionByMouse;
        }
        private void OnDestroy()
        {
            if (_inputService == null) return;
            
            _inputService.OnMouseButtonDown -= StartMovingPaddle;
            _inputService.OnMouseButtonUp -= EndMovingPaddle;
            _inputService.OnMousePositionChanged -= ChangeGoalPositionByMouse;
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

                float moveXDistance = _cameraService.GetWorldPointWidth(_camera) * _moveSpeed * Time.deltaTime;

                nextXPosition = nextXPosition + moveXDirection * moveXDistance;

                if (moveXDirection > 0) nextXPosition = Mathf.Min(nextXPosition, _goalXPosition);
                else nextXPosition = Mathf.Max(nextXPosition, _goalXPosition);


                nextPosition.x = nextXPosition;

                float distanceToGoal = Mathf.Abs(_goalXPosition - this.transform.position.x);

                Vector2 velocity = new Vector2(moveXDirection * _moveSpeed, 0);
                _rb.velocity = velocity;
                _rb.velocity = Vector2.Lerp(Vector2.zero, velocity, 
                                            (1 - (velocity.magnitude * Time.deltaTime * Mathf.Clamp01(1f - _rb.drag * Time.deltaTime)
                                                  ) / distanceToGoal));
            }
        }


        
    }
}
