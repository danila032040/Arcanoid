using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Base
{
    [RequireComponent(typeof(CatchableBoostSpawner))]
    public class CatchableBoostSpawnerBlock : DestroyableBlock
    {
        private CatchableBoostSpawner _catchableBoost;

        protected override void Awake()
        {
            base.Awake();
            _catchableBoost = this.GetComponent<CatchableBoostSpawner>();
        }

        public CatchableBoostSpawner GetCatchableBoostEffectSpawner() => _catchableBoost;

    }
}