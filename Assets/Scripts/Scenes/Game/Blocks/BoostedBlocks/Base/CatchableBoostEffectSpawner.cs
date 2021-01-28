using Scenes.Game.Balls;
using Scenes.Game.Blocks.BoostedBlocks.BallBoostEffects.AngerBall;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Base
{
    public class CatchableBoostEffectSpawner : BoostEffect
    {
        [SerializeField] private CatchableBoostEffect _catchableBoostEffectPrefab;

        private BallsManager _ballsManager;

        public void Init(BallsManager ballsManager)
        {
            _ballsManager = ballsManager;
        }

        public override void Use()
        {
            CatchableBoostEffect effect = Instantiate(_catchableBoostEffectPrefab, transform.position, Quaternion.identity);
            if (effect is AngryBallBoostEffect)
            {
                ((AngryBallBoostEffect) effect).Init(_ballsManager);
            }
        }
    }
}