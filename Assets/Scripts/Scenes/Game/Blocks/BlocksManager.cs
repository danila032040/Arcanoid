using System;
using System.Collections.Generic;
using DG.Tweening;
using SaveLoadSystem.Interfaces.Infos;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.Boosters.Base;
using Scenes.Game.Blocks.Pool;
using Scenes.Game.Contexts;
using Scenes.Game.Contexts.InitializationInterfaces;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    public class BlocksManager : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _topOffset;

        [SerializeField] private GameContext _gameContext;
        [SerializeField] private BlocksPoolManager _poolManager;


        private Block[,] _blocks;

        public Block[,] GetBlocks() => _blocks;

        public event Action<Block[,]> BlocksChanged;

        public void SpawnBlocks(IBlockLevelInfo info)
        {
            if (!(_blocks is null)) DeleteBlocks();
            int n = info.Map.GetLength(0);
            int m = info.Map.GetLength(1);

            float blockWidth = Mathf.Max(0,
                _gameContext.CameraService.GetWorldPointWidth(_gameContext.Camera) - info.LeftOffset -
                info.RightOffset -
                info.OffsetBetweenRows * (m - 1)) / m;

            _blocks = new Block[n, m];


            Vector3 startPos = _gameContext.Camera.ViewportToWorldPoint(new Vector3(0, (1 - _topOffset), 0));
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

            OnBlocksChanged(_blocks);
        }

        public Block SpawnBlock(Vector3 position, BlockType type, float blockHeight, float blockWidth)
        {
            Block res = SpawnOneBlock(position, type, blockHeight, blockWidth);
            OnBlocksChanged(_blocks);
            return res;
        }

        public void DeleteBlocks()
        {
            if (_blocks == null) return;
            foreach (Block block in _blocks)
            {
                DeleteOneBlock(block);
            }

            _blocks = null;
            OnBlocksChanged(_blocks);
        }

        public void DeleteBlock(Block block)
        {
            DeleteOneBlock(block);
            OnBlocksChanged(_blocks);
        }


        private void BlockHealthValueChanged(KeyValuePair<DestroyableBlock, int> info)
        {
            Block block = info.Key;
            int newValue = info.Value;

            if (newValue <= 0)
            {
                if (!ReferenceEquals(block, null))
                {
                    block.GetComponent<Boost>()?.Use();

                    DeleteOneBlock(block);
                    OnBlocksChanged(_blocks);
                }
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
                dBlock.HealthValueChanged += BlockHealthValueChanged;
                dBlock.GetBlockDestructibility().InitValues();
            }

            block.GetComponent<IInitContext<BoostContext>>()?.Init(_gameContext.BoostContext);

            return block;
        }

        private void DeleteOneBlock(Block block)
        {
            if (ReferenceEquals(block, null)) return;

            var dBlock = block as DestroyableBlock;
            if (!(dBlock is null))
            {
                dBlock.HealthValueChanged -= BlockHealthValueChanged;
            }

            _poolManager.Remove(block);

            for (int i = 0; i < _blocks.GetLength(0); ++i)
            {
                for (int j = 0; j < _blocks.GetLength(1); ++j)
                    if (ReferenceEquals(_blocks[i, j], block))
                    {
                        _blocks[i, j] = null;
                        return;
                    }
            }
        }

        private void OnBlocksChanged(Block[,] obj)
        {
            BlocksChanged?.Invoke(obj);
        }
    }
}