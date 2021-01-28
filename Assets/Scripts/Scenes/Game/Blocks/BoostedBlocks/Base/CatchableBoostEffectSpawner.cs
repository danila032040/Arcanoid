using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Base
{
    public class CatchableBoostEffectSpawner : BoostEffect
    {
        [SerializeField] private CatchableBoostEffect _catchableBoostEffectPrefab;

        public override void Use()
        {
            Instantiate(_catchableBoostEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}