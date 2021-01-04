namespace Scripts.Scenes.Game.Wall
{
    using UnityEngine;

    public class Wall : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;

        [SerializeField] private Vector2 _startWallViewportPoint;
        [SerializeField] private Vector2 _endWallViewportPoint;

        [SerializeField] private float _width = 1f;
        [SerializeField] private float _zPosition = 0f;

        private Camera _camera;

        public void Init(Camera camera)
        {
            _camera = camera;
        }

        private void OnValidate()
        {
            Init(Camera.main);

            Vector3 startPoint = _camera.ViewportToWorldPoint(_startWallViewportPoint);
            Vector3 endPoint = _camera.ViewportToWorldPoint(_endWallViewportPoint);

            startPoint.z = _zPosition;
            endPoint.z = _zPosition;

            startPoint.y += _collider.size.y * this.transform.localScale.y / 2;

            this.transform.position = startPoint;




            float length = (_endWallViewportPoint - _startWallViewportPoint).magnitude;

            Vector3 scale = this.transform.localScale;
            scale.x = _width;

            this.transform.localScale = scale;
        }
    }
}
