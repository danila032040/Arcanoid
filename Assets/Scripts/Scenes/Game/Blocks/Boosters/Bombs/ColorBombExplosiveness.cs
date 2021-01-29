using System.Collections.Generic;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Blocks.Boosters.Bombs.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Bombs
{
    public class ColorBombExplosiveness : BombExplosiveness
    {
        private readonly Vector2Int[] _movesToSiblings = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1)
        };

        protected override void Explode(Block[,] blocks, Vector2Int position)
        {
            foreach (Block blockToExplode in GetBlocksInTheBiggestSiblingPiece(blocks, position))
            {
                DoDamage(blockToExplode as DestroyableBlock);
            }
        }

        private List<Block> GetBlocksInTheBiggestSiblingPiece(Block[,] blocks, Vector2Int position)
        {
            List<Block> blocksInBiggestPiece = new List<Block>();
            int n = blocks.GetLength(0);
            int m = blocks.GetLength(1);

            foreach (Vector2Int moveToSibling in _movesToSiblings)
            {
                Vector2Int siblingPosition = position + moveToSibling;
                if (IsInDiapason(siblingPosition, n - 1, m - 1))
                {
                    List<Block> blocksInPiece = GetPieceOfEqualBlockType(blocks, siblingPosition);
                    if (blocksInBiggestPiece.Count < blocksInPiece.Count)
                    {
                        blocksInBiggestPiece = blocksInPiece;
                    }
                }
            }

            return blocksInBiggestPiece;
        }

        private List<Block> GetPieceOfEqualBlockType(Block[,] blocks, Vector2Int position)
        {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            List<Block> answer = new List<Block>();
            bool[,] usedBlock = new bool[blocks.GetLength(0), blocks.GetLength(1)];

            usedBlock[position.x, position.y] = true;
            answer.Add(blocks[position.x, position.y]);
            queue.Enqueue(position);

            while (queue.Count != 0)
            {
                DoPossibleMoves(blocks, queue, usedBlock, answer);
            }

            return answer;
        }


        private void DoPossibleMoves(Block[,] blocks, Queue<Vector2Int> queue, bool[,] usedBlock, List<Block> answer)
        {
            int n = usedBlock.GetLength(0);
            int m = usedBlock.GetLength(1);
            Vector2Int position = queue.Dequeue();

            foreach (Vector2Int possibleMove in _movesToSiblings)
            {
                Vector2Int newPosition = position + possibleMove;

                if (IsInDiapason(newPosition, n - 1, m - 1) && !usedBlock[newPosition.x, newPosition.y] &&
                    IsEqualBlocksType(blocks[position.x, position.y], blocks[newPosition.x, newPosition.y]))
                {
                    usedBlock[newPosition.x, newPosition.y] = true;
                    answer.Add(blocks[newPosition.x, newPosition.y]);
                    queue.Enqueue(newPosition);
                }
            }
        }

        private bool IsEqualBlocksType(Block block1,Block block2)
        {
            if (ReferenceEquals(block1,null) || ReferenceEquals(block2,null)) return false;
            return block1.GetBlockType() == block2.GetBlockType();
        }

        private bool IsInDiapason(Vector2Int value, int start, int end)
        {
            return value.x >= 0 && value.x <= start &&
                   value.y >= 0 && value.y <= end;
        }
    }
}