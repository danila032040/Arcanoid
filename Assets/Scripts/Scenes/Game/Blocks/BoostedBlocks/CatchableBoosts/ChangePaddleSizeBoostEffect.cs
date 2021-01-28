using Scenes.Game.Blocks.BoostedBlocks.Base;
using Scenes.Game.Paddles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts
{
    public class ChangePaddleSizeBoostEffect : CatchableBoostEffect
    {
        [FormerlySerializedAs("_increasePaddle")] [SerializeField] private bool _increasePaddleSize;

        [SerializeField] private float _effectDuration;
        private Paddle _paddle;
        public void Init(Paddle paddle)
        {
            _paddle = paddle;
        }
        public override void Catch()
        {
            if (_increasePaddleSize)
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