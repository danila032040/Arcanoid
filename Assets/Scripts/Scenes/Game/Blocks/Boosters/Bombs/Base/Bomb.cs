using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Bombs.Base
{
    [RequireComponent(typeof(BombExplosiveness))]
    public class Bomb : DestroyableBlock
    {
        private BombExplosiveness _bombExplosiveness;

        protected override void Awake()
        {
            base.Awake();
            _bombExplosiveness = GetComponent<BombExplosiveness>();
        }

        public BombExplosiveness GetBombExplosiveness() => _bombExplosiveness;
    }
}