using DG.Tweening;
using Scenes.Game.Effects.Base;
using Scenes.Game.Paddles;
using UnityEngine;

namespace Scenes.Game.Effects.Impl
{
    public class ChangePaddleSpeed : Effect
    {
        [SerializeField] private float _changedPaddleSpeed;
        [SerializeField] private float _changePaddleSpeedAnimationDuration;

        private float _initialSpeed;

        public override void Enable()
        {
            _initialSpeed = Context.Paddle.GetPaddleMovement().GetInitialSpeed();
            ChangeSpeed(_changedPaddleSpeed);
        }

        public override void Disable()
        {
            ChangeSpeed(_initialSpeed);
        }

        private void ChangeSpeed(float changedPaddleSpeed)
        {
            DOTween.To(() => Context.Paddle.GetPaddleMovement().GetCurrentSpeed(),
                x => { Context.Paddle.GetPaddleMovement().SetSpeed(x); }, changedPaddleSpeed,
                _changePaddleSpeedAnimationDuration);
        }
    }
}