using Scenes.Game.Blocks.Boosters.Base;
using Scenes.Game.Effects.Base;
using Scenes.Game.Effects.Impl;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.CatchableBoosts
{
    public class ChangeBallsSpeedBoost : CatchableBoost
    {
        [SerializeField] private EffectType _effectType;
        public override void Use()
        {
            Context.EffectsManager.SpawnEffect(_effectType);
            Destroy(gameObject);
        }
    }
}