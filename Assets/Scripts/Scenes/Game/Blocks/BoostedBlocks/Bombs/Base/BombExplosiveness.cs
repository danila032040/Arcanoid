using System.Collections.Generic;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.BoostedBlocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Bombs.Base
{
    public abstract class BombExplosiveness : BoostEffect
    {
        [SerializeField] private int _damage;

        private BlocksManager _blocksManager;

        public void Init(BlocksManager blocksManager)
        {
            _blocksManager = blocksManager;
        }

        public override void Use()
        {
            Block[,] blocks = _blocksManager.GetBlocks();

            Vector2Int position = FindCurrentBombPosition(blocks);
            Explode(blocks, position);
        }

        protected void DoDamage(Block block)
        {
            DestroyableBlock db = (DestroyableBlock)block;
            db.GetBlockDestructibility().AddHealth(-_damage);
        }

        private Vector2Int FindCurrentBombPosition(Block[,] blocks)
        {
            for (int i = 0; i < blocks.GetLength(0); ++i)
            {
                for (int j = 0; j < blocks.GetLength(1); ++j)
                {
                    Bomb bomb = blocks[i, j] as Bomb;
                    if (!ReferenceEquals(bomb,null) && bomb.GetBombExplosiveness() == this)
                        return new Vector2Int(i, j);
                }
            }

            throw new KeyNotFoundException();
        }


        protected abstract void Explode(Block[,] blocks, Vector2Int position);
    }
}