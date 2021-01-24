using Pool.Interfaces;
using UnityEngine;

namespace Scenes.Game.Blocks.Base
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