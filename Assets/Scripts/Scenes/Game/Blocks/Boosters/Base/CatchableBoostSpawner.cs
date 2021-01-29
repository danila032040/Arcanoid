using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Base
{
    public class CatchableBoostSpawner : Boost
    {
        [SerializeField] private CatchableBoost _catchableBoostPrefab;

        public override void Use()
        {
            CatchableBoost effect = Instantiate(_catchableBoostPrefab, transform.position, Quaternion.identity);
            effect.Init(Context);
        }
    }
}