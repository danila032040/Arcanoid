using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.BoostedBlocks.Bombs.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Bombs
{
    public class NormalBombExplosiveness : BombExplosiveness
    {
        protected override void Explode(Block[,] blocks, Vector2Int position)
        {
            int i = position.x;
            int j = position.y;

            if (i - 1 >= 0) DoDamageToSiblingsOnLine(blocks, i - 1, j);
            DoDamageToSiblingsOnLine(blocks, i, j);
            if (i + 1 <= blocks.GetLength(0) - 1) DoDamageToSiblingsOnLine(blocks, i + 1, j);
        }

        private void DoDamageToSiblingsOnLine(Block[,] blocks, int i, int j)
        {
            if (j - 1 >= 0)
            {
                DoDamage(blocks[i, j - 1]);
            }

            DoDamage(blocks[i, j]);

            if (j + 1 <= blocks.GetLength(1) - 1)
            {
                DoDamage(blocks[i, j + 1]);
            }
        }
    }
}