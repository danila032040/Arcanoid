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
            _rb.AddForce((Vector3.left + Vector3.up).normalized * GetCurrentVelocity());
        }

        public float GetCurrentVelocity() => Mathf.Lerp(_initialSpeed, _maxSpeed, _currentSpeedProgress);
    }
}
