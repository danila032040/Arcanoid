using System.Collections.Generic;
using Scenes.Game.AllTypes;
using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Pool
{
    public class BlocksPoolManager : MonoBehaviour
    {
        [SerializeField] private BlocksPool[] _pools;

        private readonly Dictionary<BlockType, BlocksPool> _dictionary = new Dictionary<BlockType, BlocksPool>();
        private void Awake()
        {
            foreach (BlocksPool pool in _pools)
            {
                _dictionary.Add(pool.GetBlockType(), pool);
            }
        }

        public Block Get(BlockType type) => GetPool(type).Get();

        public void Remove(Block block) => GetPool(block.GetBlockType()).Remove(block);

        private BlocksPool GetPool(BlockType type) => _dictionary[type];
    }
}