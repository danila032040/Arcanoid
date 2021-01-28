using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Base
{
    [RequireComponent(typeof(CatchableBoostEffectSpawner))]
    public class CatchableBoostEffectSpawnerBlock : DestroyableBlock
    {
        private CatchableBoostEffectSpawner _catchableBoostEffect;

        protected override void Awake()
        {
            base.Awake();
            _catchableBoostEffect = this.GetComponent<CatchableBoostEffectSpawner>();
        }

        public CatchableBoostEffectSpawner GetCatchableBoostEffectSpawner() => _catchableBoostEffect;

    }
}