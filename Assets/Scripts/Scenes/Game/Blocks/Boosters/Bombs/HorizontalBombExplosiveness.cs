using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.Boosters.Bombs.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Bombs
{
    public class HorizontalBombExplosiveness : BombExplosiveness
    {
        protected override void Explode(Block[,] blocks, Vector2Int position)
        {
            int i = position.x;
            int j = position.y;
            while (--j >= 0)
            {
                DoDamage(blocks[i, j] as DestroyableBlock);
            }

            j = position.y;
            while (++j <= blocks.GetLength(1) - 1)
            {
                DoDamage(blocks[i, j] as DestroyableBlock);
            }
        }
    }
}