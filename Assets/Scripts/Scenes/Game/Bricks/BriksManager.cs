using System;
using SaveLoadSystem.Interfaces.Infos;
using Scenes.Game.Bricks;
using Scenes.Game.Services.Cameras.Implementations;
using Scenes.Game.Services.Cameras.Interfaces;

namespace Scripts.Scenes.Game.Bricks
{
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

        public void Awake()
        {
            Init(new CameraService(), Camera.main);
        }

        public void SpawnBricks(IBrickLevelInfo info)
        {
            int n = info.Map.GetLength(0);
            int m = info.Map.GetLength(1);

            float brickWidth = Mathf.Max(0,
                _cameraService.GetWorldPointWidth(_camera) - info.LeftOffset - info.RightOffset -
                info.OffsetBetweenRows * (m - 1)) / m;

            _bricks = new Brick[n, m];


            Vector3 startPos = _camera.ViewportToWorldPoint(new Vector3(0, (1 - _topOffset), 0));
            startPos.z = 0;
            startPos.x += info.LeftOffset;

            for (int i = 0; i < n; ++i)
            for (int j = 0; j < m; ++j)
            {
                Vector3 currPosition = startPos;
                currPosition.x += j * (brickWidth + info.OffsetBetweenRows) + brickWidth / 2;
                currPosition.y -= i * (info.BrickHeight + info.OffsetBetweenCols) + info.BrickHeight / 2;

                _bricks[i, j] = SpawnBrick(currPosition, info.Map[i, j], info.BrickHeight, brickWidth);
            }
        }

        public void DeleteBricks()
        {
            foreach (Brick brick in _bricks)
            {
                DeleteBrick(brick);
            }
        }


        //TODO: Make Pool 
        [SerializeField] private Brick _prefab;

        public Brick SpawnBrick(Vector3 position, BrickType? type, float brickHeight, float brickWidth)
        {
            if (type == null) return null;

            Brick brick = Instantiate(_prefab, position, Quaternion.identity);

            brick.Size = new Vector3(brickWidth, brickHeight, 0);

            brick.OnHealthValueChanged += BrickOnOnHealthValueChanged;
            return brick;
        }

        private void BrickOnOnHealthValueChanged(object sender, int oldvalue, int newvalue)
        {
            if (newvalue <= 0)
            {
                DeleteBrick(sender as Brick);
            }
        }


        //TODO: Make Pool 
        public void DeleteBrick(Brick brick)
        {
            if (brick == null) return;
            Destroy(brick.gameObject);

            for (int i = 0; i < _bricks.GetLength(0); ++i)
                for (int j = 0; j < _bricks.GetLength(1); ++j)
                    if (_bricks[i, j] == brick)
                    {
                        _bricks[i, j] = null;
                        return;
                    }
        }
    }
}