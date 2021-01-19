using SaveLoadSystem.Interfaces.Infos;
using Scenes.Game.Services.Cameras.Implementations;
using Scenes.Game.Services.Cameras.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.Game.Blocks
{
    public class BlocksManager : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _topOffset;

        private Block[,] _blocks;

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

        public void SpawnBlocks(IBlockLevelInfo info)
        {
            int n = info.Map.GetLength(0);
            int m = info.Map.GetLength(1);

            float blockWidth = Mathf.Max(0,
                _cameraService.GetWorldPointWidth(_camera) - info.LeftOffset - info.RightOffset -
                info.OffsetBetweenRows * (m - 1)) / m;

            _blocks = new Block[n, m];


            Vector3 startPos = _camera.ViewportToWorldPoint(new Vector3(0, (1 - _topOffset), 0));
            startPos.z = 0;
            startPos.x += info.LeftOffset;

            for (int i = 0; i < n; ++i)
            for (int j = 0; j < m; ++j)
            {
                Vector3 currPosition = startPos;
                currPosition.x += j * (blockWidth + info.OffsetBetweenRows) + blockWidth / 2;
                currPosition.y -= i * (info.BlockHeight + info.OffsetBetweenCols) + info.BlockHeight / 2;

                _blocks[i, j] = SpawnBlock(currPosition, info.Map[i, j], info.BlockHeight, blockWidth);
            }
        }

        public void DeleteBlocks()
        {
            foreach (Block block in _blocks)
            {
                DeleteBlock(block);
            }
        }


        //TODO: Make Pool 
        [SerializeField] private Block _prefab;

        public Block SpawnBlock(Vector3 position, BlockType? type, float blockHeight, float blockWidth)
        {
            if (type == null) return null;

            Block block = Instantiate(_prefab, position, Quaternion.identity);

            block.Size = new Vector3(blockWidth, blockHeight, 0);

            block.OnHealthValueChanged += BlockOnOnHealthValueChanged;
            return block;
        }

        private void BlockOnOnHealthValueChanged(object sender, int oldvalue, int newvalue)
        {
            if (newvalue <= 0)
            {
                DeleteBlock(sender as Block);
            }
        }


        //TODO: Make Pool 
        public void DeleteBlock(Block block)
        {
            if ((Object)block == null) return;
            Destroy(block.gameObject);

            for (int i = 0; i < _blocks.GetLength(0); ++i)
                for (int j = 0; j < _blocks.GetLength(1); ++j)
                    if (_blocks[i, j] == block)
                    {
                        _blocks[i, j] = null;
                        return;
                    }
        }
    }
}