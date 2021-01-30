using DG.Tweening;
using Scenes.Game.Effects.Base;
using UnityEngine;

namespace Scenes.Game.Effects.Impl
{
    public class ChangePaddleSize : Effect
    {
        [SerializeField] private float _changedPaddleSizeCoefficient;
        [SerializeField] private float _changePaddleSizeAnimationDuration;

        private Vector3 _initialSize;

        public override void Enable()
        {
            _initialSize = Context.Paddle.GetPaddleView().GetInitialScale();

            Vector3 changedSize = _changedPaddleSizeCoefficient * _initialSize;
            changedSize.y = _initialSize.y;
            changedSize.z = _initialSize.z;

            ChangeSpeed(changedSize, _changePaddleSizeAnimationDuration);
        }

        public override void ForceEnable()
        {
            _initialSize = Context.Paddle.GetPaddleView().GetInitialScale();

            Vector3 changedSize = _changedPaddleSizeCoefficient * _initialSize;
            changedSize.y = _initialSize.y;
            changedSize.z = _initialSize.z;

            ChangeSpeed(changedSize, 0f);
        }

        public override void Disable()
        {
            ChangeSpeed(_initialSize, _changePaddleSizeAnimationDuration);
        }

        public override void ForceDisable()
        {
            ChangeSpeed(_initialSize, 0f);
        }

        private void ChangeSpeed(Vector3 changedPaddleSize, float duration)
        {
            if (duration != 0f)
            {
                DOTween.To(() => Context.Paddle.GetPaddleView().GetCurrentScale(),
                    x => { Context.Paddle.GetPaddleView().SetScale(x); }, changedPaddleSize,
                    duration);
            }
            else
            {
                Context.Paddle.GetPaddleView().SetScale(changedPaddleSize);
            }
        }
    }
}