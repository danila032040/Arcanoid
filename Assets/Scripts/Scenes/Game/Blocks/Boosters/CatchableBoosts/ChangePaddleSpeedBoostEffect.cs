using Scenes.Game.AllTypes;
using Scenes.Game.Blocks.Boosters.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.CatchableBoosts
{
    public class ChangePaddleSpeedBoostEffect : CatchableBoost
    {
        [SerializeField] private EffectType _effectType;

        public override void Use()
        {
            Context.EffectsManager.SpawnEffect(_effectType);
            Destroy(gameObject);
        }
    }
}