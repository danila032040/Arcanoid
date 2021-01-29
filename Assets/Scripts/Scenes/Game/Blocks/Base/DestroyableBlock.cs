using System;
using System.Collections.Generic;
using DG.Tweening;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks.Base
{
    [RequireComponent(typeof(BlockDestructibility))]
    public class DestroyableBlock : Block
    {
        private BlockDestructibility _blockDestructibility;

        public event Action<KeyValuePair<DestroyableBlock, int>> HealthValueChanged;

        protected override void Awake()
        {
            base.Awake();

            _blockDestructibility = GetComponent<BlockDestructibility>();

            _blockDestructibility.HealthValueChanged += (value, newValue) =>
            {
                GetBlockView().GetSpriteRenderer().DOFade(_blockDestructibility.GetHealthPercentage(), 0f);
                OnHealthValueChanged(newValue);
            };
        }

        public BlockDestructibility GetBlockDestructibility() => _blockDestructibility;

        private void OnHealthValueChanged( int newValue)
        {
            HealthValueChanged?.Invoke(new KeyValuePair<DestroyableBlock, int>(this, newValue));
        }
    }
}