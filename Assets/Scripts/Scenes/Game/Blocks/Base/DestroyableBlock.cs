using DG.Tweening;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks.Base
{
    [RequireComponent(typeof(BlockDestructibility))]
    public class DestroyableBlock : Block
    {
        private BlockDestructibility _blockDestructibility;

        public event OnIntValueChanged HealthValueChanged;

        protected override void Awake()
        {
            base.Awake();

            _blockDestructibility = GetComponent<BlockDestructibility>();

            _blockDestructibility.HealthValueChanged += (sender, value, newValue) =>
            {
                GetBlockView().GetSpriteRenderer().DOFade(_blockDestructibility.GetHealthPercentage(), 0f);
                OnHealthValueChanged(value, newValue);
            };
        }

        public BlockDestructibility GetBlockDestructibility() => _blockDestructibility;

        private void OnHealthValueChanged(int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(this, oldValue, newValue);
        }
    }
}