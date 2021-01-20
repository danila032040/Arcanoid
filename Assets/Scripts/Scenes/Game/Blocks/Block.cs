using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    [RequireComponent(typeof(BlockView))]
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockType _type;

        private BlockView _blockView;

        protected virtual void Awake()
        {
            _blockView = GetComponent<BlockView>();
        }

        public BlockView GetBlockView() => _blockView;
    }
}