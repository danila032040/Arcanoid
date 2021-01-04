namespace Scripts.Scenes.Game.Wall
{
    using Scripts.Scenes.Game.Input;
    using UnityEngine;

    public class Wall : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;

        [SerializeField] private Vector2 _startWallViewportPoint;
        [SerializeField] private Vector2 _endWallViewportPoint;

        [SerializeField] private float _width = 1f;
        [SerializeField] private float _zPosition = 0f;

        private IInputService _inputService;
        private Camera _camera;

        public void Init(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;
        }

        private void Start()
        {
            Init(FindObjectOfType<InputService>(), Camera.main);

            _inputService.OnScreenResolutionChanged += (orientation) => ArrangeWall();

            ArrangeWall();
        }

        private void OnValidate()
        {
            Start();
        }

        private void ArrangeWall()
        {
            Vector3 startPoint = _camera.ViewportToWorldPoint(_startWallViewportPoint);
            Vector3 endPoint = _camera.ViewportToWorldPoint(_endWallViewportPoint);

            startPoint.z = _zPosition;
            endPoint.z = _zPosition;


            Vector3 direction = endPoint - startPoint;



            Vector3 scale = this.transform.localScale;
            scale.x = _width;
            scale.y = direction.magnitude;

            this.transform.localScale = scale;
            this.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Sign(direction.x) * Vector3.Angle(Vector3.up, direction));
            this.transform.position = startPoint;
        }
    }
}
