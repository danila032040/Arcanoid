namespace Scripts.Scenes.Game.Ball
{
    using UnityEngine;

    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;


        [SerializeField] private float _initialSpeed;
        [SerializeField] private float _maxSpeed;

        private float _currentSpeedProgress = 0f;

        private void Start()
        {
            _rb.isKinematic = false;
            _rb.velocity = (Vector2.left + Vector2.up).normalized * GetCurrentVelocity();
        }

        private void Update()
        {
            _rb.velocity = _rb.velocity.normalized * GetCurrentVelocity();
        }

        public float GetCurrentVelocity() => Mathf.Lerp(_initialSpeed, _maxSpeed, _currentSpeedProgress);


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Paddle")
            {
                _rb.velocity = Vector2.zero;

                float x = (this.transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;

                Vector2 direction = new Vector2(x, 1).normalized;

                _rb.velocity = direction * GetCurrentVelocity();
            }
        }
    }
}
