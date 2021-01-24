using Scenes.Game.Services.Screens.Implementations;
using Scenes.Game.Services.Screens.Interfaces;
using UnityEngine;

namespace Scenes.Game.Walls
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;

        [SerializeField] private Vector2 _startWallViewportPoint;
        [SerializeField] private Vector2 _endWallViewportPoint;

        [SerializeField] private float _width = 1f;
        [SerializeField] private float _zPosition = 0f;

        private IScreenService _screenService;
        private Camera _camera;

        public void Init(IScreenService screenService, Camera camera)
        {
            _screenService = screenService;
            _camera = camera;
        }

        [SerializeField] private ScreenService _screenServiceImpl;
        private void Start()
        {
            Init(_screenServiceImpl, Camera.main);

            _screenService.ScreenResolutionChanged += (orientation) => ArrangeWall();

            ArrangeWall();
        }
        
        private void ArrangeWall()
        {
            Vector3 startPoint = _camera.ViewportToWorldPoint(_startWallViewportPoint);
            Vector3 endPoint = _camera.ViewportToWorldPoint(_endWallViewportPoint);

            startPoint.z = _zPosition;
            endPoint.z = _zPosition;


            Vector3 direction = endPoint - startPoint;


            var wallTransform = this.transform;

            Vector3 scale = wallTransform.localScale;
            scale.x = _width;
            scale.y = direction.magnitude;

            wallTransform.localScale = scale;
            wallTransform.rotation =
                Quaternion.Euler(0, 0, -Mathf.Sign(direction.x) * Vector3.Angle(Vector3.up, direction));
            wallTransform.position = startPoint;
        }
    }
}