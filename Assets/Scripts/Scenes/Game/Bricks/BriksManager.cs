namespace Scripts.Scenes.Game.Bricks
{
    using Scripts.Scenes.Game.Camera.Implementations;
    using Scripts.Scenes.Game.Camera.Intrefaces;
    using UnityEngine;
    public class BriksManager : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _topOffset;

        private Brick[,] _bricks;

        private ICameraService _cameraService;
        private Camera _camera;

        public void Init(ICameraService cameraService, Camera camera)
        {
            _cameraService = cameraService;
            _camera = camera;
        }
        public void Start()
        {
            Init(new CameraService(), Camera.main);
        }

        public void SpawnBricks(BrickType?[,] map, float brickHeight, float leftOffset, float rightOffset, float offsetBetweenRows, float offsetBetweenCols)
        {
            int n = map.GetLength(0);
            int m = map.GetLength(1);

            float brickWidth = Mathf.Max(0, _cameraService.GetWorldPointWidth(_camera) - leftOffset - rightOffset - offsetBetweenRows * (m - 1)) / m;

            _bricks = new Brick[n, m];


            Vector3 startPos = _camera.ViewportToWorldPoint(new Vector3(0, (1 - _topOffset), 0));
            startPos.z = 0;
            startPos.x += leftOffset;

            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                {
                    Vector3 currPosition = startPos;
                    currPosition.x += j * (brickWidth + offsetBetweenRows) + brickWidth / 2;
                    currPosition.y -= i * (brickHeight + offsetBetweenCols) + brickHeight / 2;

                    _bricks[i, j] = SpawnBrick(currPosition, map[i, j], brickHeight, brickWidth);
                }

        }

        private Brick SpawnBrick(Vector3 position, BrickType? type, float brickHeight, float brickWidth)
        {
            if (type == null) return null;

            return null;
        }
    }
}
