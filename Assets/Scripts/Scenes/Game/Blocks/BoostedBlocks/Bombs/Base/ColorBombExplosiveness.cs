using System.Collections.Generic;
using System.Linq;
using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Bombs.Base
{
    public class ColorBombExplosiveness : BombExplosiveness
    {
        private readonly Dictionary<BlockType, int> _blocksCount = new Dictionary<BlockType, int>();

        protected override void Explode(Block[,] blocks, Vector2Int position)
        {
            int i = position.x;
            int j = position.y;

            if (i - 1 >= 0) ++_blocksCount[blocks[i - 1, j].GetBlockType()];
            if (i + 1 <= blocks.GetLength(0) - 1) ++_blocksCount[blocks[i + 1, j].GetBlockType()];

            if (j - 1 >= 0) ++_blocksCount[blocks[i, j - 1].GetBlockType()];
            if (j + 1 <= blocks.GetLength(1) - 1) ++_blocksCount[blocks[i, j + 1].GetBlockType()];

            //base.DoDamage(blocks[i-1,j]);
        }
    }
}