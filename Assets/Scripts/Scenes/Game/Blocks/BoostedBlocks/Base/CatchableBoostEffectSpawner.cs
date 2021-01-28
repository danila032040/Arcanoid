using Scenes.Game.Balls;
using Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts;
using Scenes.Game.Paddles;
using Scenes.Game.Player;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.Base
{
    public class CatchableBoostEffectSpawner : BoostEffect
    {
        [SerializeField] private CatchableBoostEffect _catchableBoostEffectPrefab;

        private BallsManager _ballsManager;
        private HpController _hpController;
        private Paddle _paddle;

        public void Init(BallsManager ballsManager, HpController hpController, Paddle paddle)
        {
            _ballsManager = ballsManager;
            _hpController = hpController;
            _paddle = paddle;
        }

        public override void Use()
        {
            CatchableBoostEffect effect = Instantiate(_catchableBoostEffectPrefab, transform.position, Quaternion.identity);
            
            (effect as AngryBallBoostEffect)?.Init(_ballsManager);
            (effect as ChangeBallsSpeedBoostEffect)?.Init(_ballsManager);
            
            (effect as ChangePaddleSizeBoostEffect)?.Init(_paddle);
            (effect as ChangePaddleSpeedBoostEffect)?.Init(_paddle);
            
            (effect as ChangeHealthBoostEffect)?.Init(_hpController);
        }
    }
}