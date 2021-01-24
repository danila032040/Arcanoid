using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.BoostedBlocks.Bombs.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Bombs
{
    public class VerticalBombExplosiveness : BombExplosiveness
    {
        protected override void Explode(Block[,] blocks, Vector2Int position)
        {
            int i = position.x;
            int j = position.y;
            while (i >= 0)
            {
                DoDamage(blocks[i, j]);
                --i;
            }

            i = position.x;
            while (i <= blocks.GetLength(0) - 1)
            {
                DoDamage(blocks[i, j]);
                ++i;
            }
        }
    }
}