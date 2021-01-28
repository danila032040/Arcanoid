using Scenes.Game.Blocks.BoostedBlocks.Base;
using Scenes.Game.Paddles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts
{
    public class ChangePaddleSpeedBoostEffect : CatchableBoostEffect
    {
        [FormerlySerializedAs("_increaseSpeed")] [SerializeField] private bool _increasePaddleSpeed;
        [SerializeField] private float _effectDuration;

        private Paddle _paddle;

        public void Init(Paddle paddle)
        {
            _paddle = paddle;
        }

        public override void Catch()
        {
            if (_increasePaddleSpeed)
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