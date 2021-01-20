using DG.Tweening;
using Pool.Interfaces;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    [RequireComponent(typeof(BlockView))]
    public class Block : MonoBehaviour, IPoolable
    {
        [SerializeField] private BlockType _type;

        private BlockView _blockView;

        protected virtual void Awake()
        {
            _blockView = GetComponent<BlockView>();
        }

        public BlockType GetBlockType() => _type;

        public BlockView GetBlockView() => _blockView;
        
    }
}