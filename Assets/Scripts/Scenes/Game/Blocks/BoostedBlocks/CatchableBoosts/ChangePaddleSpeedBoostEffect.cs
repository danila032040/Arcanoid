using Scenes.Game.Blocks.BoostedBlocks.Base;
using Scenes.Game.Paddles;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts
{
    public class ChangePaddleSpeedBoostEffect : CatchableBoostEffect
    {
        [SerializeField] private bool _increaseSpeed;
        [SerializeField] private float _effectDuration;

        private Paddle _paddle;

        public void Init(Paddle paddle)
        {
            _paddle = paddle;
        }

        public override void Catch()
        {
            if (_increaseSpeed)
            {
                _paddle.GetPaddleMovement().IncreaseSpeed(_effectDuration);
            }
            else
            {
                _paddle.GetPaddleMovement().DecreaseSpeed(_effectDuration);
            }
            Destroy(gameObject);
        }
    }
}