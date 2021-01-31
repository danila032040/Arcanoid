using DG.Tweening;
using Scenes.Game.Effects.Base;
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
            ChangeSpeed(_changedPaddleSpeed, _changePaddleSpeedAnimationDuration);
        }

        public override void ForceEnable()
        {
            _initialSpeed = Context.Paddle.GetPaddleMovement().GetInitialSpeed();
            ChangeSpeed(_changedPaddleSpeed, 0f);
        }


        public override void Disable()
        {
            ChangeSpeed(_initialSpeed, _changePaddleSpeedAnimationDuration);
        }

        public override void ForceDisable()
        {
            ChangeSpeed(_initialSpeed, 0f);
        }

        private void ChangeSpeed(float changedPaddleSpeed, float duration)
        {
            if (duration != 0f)
            {
                DOTween.To(() => Context.Paddle.GetPaddleMovement().GetCurrentSpeed(),
                    x => { Context.Paddle.GetPaddleMovement().SetSpeed(x); }, changedPaddleSpeed,
                    duration);
            }
            else
            {
                Context.Paddle.GetPaddleMovement().SetSpeed(changedPaddleSpeed);
            }
        }
    }
}