using Scenes.Game.Paddles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Game.Balls.Base
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;


        [SerializeField] private float _initialSpeed;
        [SerializeField] private float _maxSpeed;

        private float _currentSpeedProgress;

        private void Update()
        {
            NormalizeVelocity(_rb.velocity.normalized);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckCollisionWithOtherObjects(collision);
            CheckCollisionWithPaddle(collision);
        }

        private void CheckCollisionWithOtherObjects(Collision2D collision)
        {
            ReflectBallVelocity(collision);
        }


        private void ReflectBallVelocity(Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 velocity = -collision.relativeVelocity;

            _rb.velocity = Vector2.Reflect(velocity, normal);
            //CorrectVelocity();
        }


        private void CheckCollisionWithPaddle(Collision2D collision)
        {
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();
            if (!paddle) return;

            if (Vector2.Angle(collision.GetContact(0).normal, Vector2.up) > 60f) return;

            _rb.velocity = Vector2.zero;

            float x = (this.transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;

            Vector2 direction = new Vector2(x, 0.75f).normalized;

            _rb.velocity = direction * GetCurrentVelocity();
        }


        [SerializeField] private float _angleWithHorToChangeDirection;
        [SerializeField] private float _minHorChangeAngle;
        [SerializeField] private float _maxHorChangeAngle;

        [SerializeField] private float _angleWithVertToChangeDirection;
        [SerializeField] private float _minVertChangeAngle;
        [SerializeField] private float _maxVertChangeAngle;

        private void NormalizeVelocity(Vector2 direction)
        {
            _rb.velocity = direction.normalized * GetCurrentVelocity();
        }

        private void CorrectVelocity()
        {
            if (Vector2.Angle(_rb.velocity, Vector2.left) <= _angleWithHorToChangeDirection ||
                Vector2.Angle(_rb.velocity, Vector2.right) <= _angleWithHorToChangeDirection)
            {
                _rb.velocity = Quaternion.Euler(0, 0, Random.Range(_minHorChangeAngle, _maxHorChangeAngle)) *
                               _rb.velocity;
            }

            if (Vector2.Angle(_rb.velocity, Vector2.up) <= _angleWithVertToChangeDirection ||
                Vector2.Angle(_rb.velocity, Vector2.down) <= _angleWithVertToChangeDirection)
            {
                _rb.velocity = Quaternion.Euler(0, 0, Random.Range(_minVertChangeAngle, _maxVertChangeAngle)) *
                               _rb.velocity;
            }
        }

        private float GetCurrentVelocity() => Mathf.Lerp(_initialSpeed, _maxSpeed, _currentSpeedProgress);


        public void StartMoving()
        {
            NormalizeVelocity(Vector2.up);
        }

        public void SetCurrentSpeedProgress(float value) => _currentSpeedProgress = value;
    }
}