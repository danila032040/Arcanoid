using System.Collections.Generic;
using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Pool
{
    public class BlocksPoolManager : MonoBehaviour
    {
        [SerializeField] private BlocksPool[] _pools;

        public Block Get(BlockType type) => GetPool(type).Get();

        public void Remove(Block block) => GetPool(block.GetBlockType()).Remove(block);

        private BlocksPool GetPool(BlockType type)
        {
            foreach (BlocksPool pool in _pools)
            {
                if (pool.GetBlockType() == type) return pool;
            }

            throw new KeyNotFoundException(type.ToString());
        }
    }
}