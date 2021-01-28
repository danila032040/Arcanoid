using Scenes.Game.Blocks.BoostedBlocks.Base;
using Scenes.Game.Paddles;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts
{
    public class ChangePaddleSizeBoostEffect : CatchableBoostEffect
    {
        [SerializeField] private bool _increasePaddle;

        [SerializeField] private float _effectDuration;
        private Paddle _paddle;
        public void Init(Paddle paddle)
        {
            _paddle = paddle;
        }
        public override void Catch()
        {
            if (_increasePaddle)
            {
                _paddle.GetPaddleView().IncreasePaddleSize(_effectDuration);
            }
            else
            {
                _paddle.GetPaddleView().DecreasePaddleSize(_effectDuration);
            }
            Destroy(gameObject);
        }

        
    }
}