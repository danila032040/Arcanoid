using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    [RequireComponent(typeof(BlockDestructibility))]
    public class DestroyableBlock : Block
    {
        private BlockDestructibility _blockDestructibility;

        public event OnIntValueChanged OnHealthValueChanged;

        protected override void Awake()
        {
            base.Awake();

            _blockDestructibility = GetComponent<BlockDestructibility>();
            _blockDestructibility.OnHealthValueChanged += (sender, value, newValue) =>
            {
                OnHealthValueChanged?.Invoke(this, value, newValue);
            };
        }

        public BlockDestructibility GetBlockDestructibility() => _blockDestructibility;
    }
}