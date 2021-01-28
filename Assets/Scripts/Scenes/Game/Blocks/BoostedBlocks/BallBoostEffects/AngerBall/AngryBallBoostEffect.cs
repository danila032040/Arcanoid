using Scenes.Game.Balls;
using Scenes.Game.Blocks.BoostedBlocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.BallBoostEffects.AngerBall
{
    public class AngryBallBoostEffect : CatchableBoostEffect
    {
        [SerializeField] private float _angryBallDuration;
        
        private BallsManager _ballsManager;
        public void Init(BallsManager ballsManager)
        {
            _ballsManager = ballsManager;
        }
        public override void Catch()
        {
            _ballsManager.AngryBall(_angryBallDuration);
            Destroy(this.gameObject);
        }
    }
}