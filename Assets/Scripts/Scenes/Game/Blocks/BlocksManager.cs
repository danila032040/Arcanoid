using System;
using DG.Tweening;
using SaveLoadSystem.Interfaces.Infos;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.BoostedBlocks.Bombs.Base;
using Scenes.Game.Blocks.Pool;
using Scenes.Game.Services.Cameras.Implementations;
using Scenes.Game.Services.Cameras.Interfaces;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Scenes.Game.Blocks
{
    public class BlocksManager : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _topOffset;

        private Block[,] _blocks;

        private ICameraService _cameraService;
        private Camera _camera;
        private BlocksPoolManager _poolManager;

        public Block[,] GetBlocks() => _blocks;

        public event Action<Block[,]> OnBlocksChanged;

        public void Init(ICameraService cameraService, Camera camera, BlocksPoolManager poolManager)
        {
            _cameraService = cameraService;
            _camera = camera;
            _poolManager = poolManager;
        }

        [SerializeField] private BlocksPoolManager _poolManagerImpl;
        public void Awake()
        {
            Init(new CameraService(), Camera.main, _poolManagerImpl);
        }

        public void SpawnBlocks(IBlockLevelInfo info)
        {
            if (!(_blocks is null)) DeleteBlocks();
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
            {
                for (int j = 0; j < m; ++j)
                {
                    Vector3 currPosition = startPos;
                    currPosition.x += j * (blockWidth + info.OffsetBetweenRows) + blockWidth / 2;
                    currPosition.y -= i * (info.BlockHeight + info.OffsetBetweenCols) + info.BlockHeight / 2;

                    _blocks[i, j] = SpawnOneBlock(currPosition, info.Map[i, j], info.BlockHeight, blockWidth);
                }
            }
            OnBlocksChanged?.Invoke(_blocks);
        }

        public Block SpawnBlock(Vector3 position, BlockType type, float blockHeight, float blockWidth)
        {
            Block res = SpawnOneBlock(position, type, blockHeight, blockWidth);
            OnBlocksChanged?.Invoke(_blocks);
            return res;
        }

        public void DeleteBlocks()
        {
            foreach (Block block in _blocks)
            {
                DeleteOneBlock(block);
            }

            _blocks = null;
            OnBlocksChanged?.Invoke(_blocks);
        }

        public void DeleteBlock(Block block)
        {
            DeleteOneBlock(block);
            OnBlocksChanged?.Invoke(_blocks);
        }


        

        private void BlockOnOnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            if (newValue <= 0)
            {
                DeleteOneBlock(sender as Block);
                OnBlocksChanged?.Invoke(_blocks);
            }
        }

        private Block SpawnOneBlock(Vector3 position, BlockType type, float blockHeight, float blockWidth)
        {
            if (type == BlockType.None) return null;

            Block block = _poolManager.Get(type);
            
            var blockTransform = block.gameObject.transform;
            blockTransform.position = position;
            blockTransform.rotation = Quaternion.identity;

            block.GetBlockView().Size = new Vector3(blockWidth, blockHeight, 0);
            block.GetBlockView().GetSpriteRenderer().DOFade(1f, 0f);

            var dBlock = block as DestroyableBlock;
            if (!(dBlock is null))
            {
                dBlock.OnHealthValueChanged += BlockOnOnHealthValueChanged;
                dBlock.GetBlockDestructibility().InitValues();
            }

            var bBlock = block as Bomb;
            bBlock?.GetBombExplosiveness().Init(this);

            return block;
        }
        private void DeleteOneBlock(Block block)
        {
            if (ReferenceEquals(block, null)) return;
            
            var dBlock = block as DestroyableBlock;
            if (!(dBlock is null))
            {
                dBlock.OnHealthValueChanged -= BlockOnOnHealthValueChanged;
            }

            var bBlock = block as Bomb;
            bBlock?.GetBombExplosiveness().Use();

            _poolManager.Remove(block);

            for (int i = 0; i < _blocks.GetLength(0); ++i)
            {
                for (int j = 0; j < _blocks.GetLength(1); ++j)
                    if (_blocks[i, j] == block)
                    {
                        _blocks[i, j] = null;
                        return;
                    }
            }
        }
    }
}