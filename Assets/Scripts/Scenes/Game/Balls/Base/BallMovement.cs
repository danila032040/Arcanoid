using Scenes.Game.Paddles;
using UnityEngine;

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

            if (!CheckCollisionWithPaddle(collision)) return;
            
            CheckCollisionWithBall(collision);
            ReflectBallVelocity(collision);

        }
        private void CheckCollisionWithBall(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (!ball) return;

            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }
        

        private void ReflectBallVelocity(Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 velocity = -collision.relativeVelocity;

            _rb.velocity = Vector2.Reflect(velocity, normal);
        }
        

        private bool CheckCollisionWithPaddle(Collision2D collision)
        {
            
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();
            if (!paddle) return true;
            
            if (collision.GetContact(0).normal != Vector2.up) return false;

            _rb.velocity = Vector2.zero;

            float x = (this.transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;

            Vector2 direction = new Vector2(x, 1).normalized;

            _rb.velocity = direction * GetCurrentVelocity();
            return true;
        }


        [SerializeField] private float _angleWithHorToChangeDirection;
        [SerializeField] private float _minChangeAngle;
        [SerializeField] private float _maxChangeAngle;
        private void NormalizeVelocity(Vector2 direction)
        {
            _rb.velocity = direction.normalized * GetCurrentVelocity();

            if (Vector2.Angle(_rb.velocity, Vector2.left) <= _angleWithHorToChangeDirection ||
                Vector2.Angle(_rb.velocity, Vector2.right) <= _angleWithHorToChangeDirection)
            {
                _rb.velocity = Quaternion.Euler(0, 0, Random.Range(_minChangeAngle, _maxChangeAngle)) * _rb.velocity;
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