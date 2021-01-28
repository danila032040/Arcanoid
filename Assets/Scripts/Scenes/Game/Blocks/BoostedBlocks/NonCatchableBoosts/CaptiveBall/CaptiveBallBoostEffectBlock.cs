using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.NonCatchableBoosts.CaptiveBall
{
    [RequireComponent(typeof(CaptiveBallBoostEffect))]
    public class CaptiveBallBoostEffectBlock : DestroyableBlock
    {
        private CaptiveBallBoostEffect _effect;

        protected override  void Awake()
        {
            base.Awake();
            _effect = GetComponent<CaptiveBallBoostEffect>();
        }

        public CaptiveBallBoostEffect GetCaptiveBallBoostEffect() => _effect;

    }
}