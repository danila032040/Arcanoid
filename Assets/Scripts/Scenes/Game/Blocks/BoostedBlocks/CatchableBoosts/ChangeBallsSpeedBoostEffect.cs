using Scenes.Game.Balls;
using Scenes.Game.Blocks.BoostedBlocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts
{
    public class ChangeBallsSpeedBoostEffect : CatchableBoostEffect
    {
        [SerializeField] private bool _increaseBallsSpeed;
        
        [SerializeField] private float _effectDuration;
        private BallsManager _ballsManager;


        public void Init(BallsManager ballsManager)
        {
            _ballsManager = ballsManager;
        }

        public override void Catch()
        {
            if (_increaseBallsSpeed)
            {
                _ballsManager.IncreaseSpeed(_effectDuration);
            }
            else
            {
                _ballsManager.DecreaseSpeed(_effectDuration);
            }
            Destroy(gameObject);
        }
    }
}