using System.Collections.Generic;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.Boosters.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Bombs.Base
{
    public abstract class BombExplosiveness : Boost
    {
        [SerializeField] private int _damage;

        public override void Use()
        {
            Block[,] blocks = Context.BlocksManager.GetBlocks();

            Vector2Int position = FindCurrentBombPosition(blocks);
            Explode(blocks, position);
        }

        protected void DoDamage(DestroyableBlock block)
        {
            if (!ReferenceEquals(block, null) && !ReferenceEquals(block.gameObject, gameObject))
                block.GetBlockDestructibility().AddHealth(-_damage);
        }

        private Vector2Int FindCurrentBombPosition(Block[,] blocks)
        {
            for (int i = 0; i < blocks.GetLength(0); ++i)
            {
                for (int j = 0; j < blocks.GetLength(1); ++j)
                {
                    Bomb bomb = blocks[i, j] as Bomb;
                    if (!ReferenceEquals(bomb, null) && bomb.GetBombExplosiveness() == this)
                        return new Vector2Int(i, j);
                }
            }

            throw new KeyNotFoundException();
        }


        protected abstract void Explode(Block[,] blocks, Vector2Int position);
    }
}