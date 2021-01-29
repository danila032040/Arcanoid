using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.NonCatchableBoosts.CaptiveBall
{
    [RequireComponent(typeof(CaptiveBallBoost))]
    public class CaptiveBallBoostBlock : DestroyableBlock
    {
        private CaptiveBallBoost _effect;

        protected override  void Awake()
        {
            base.Awake();
            _effect = GetComponent<CaptiveBallBoost>();
        }

        public CaptiveBallBoost GetCaptiveBallBoostEffect() => _effect;

    }
}