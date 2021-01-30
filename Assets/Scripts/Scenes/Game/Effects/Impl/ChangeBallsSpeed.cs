using System.Collections.Generic;
using DG.Tweening;
using Scenes.Game.Balls.Base;
using Scenes.Game.Effects.Base;
using UnityEngine;

namespace Scenes.Game.Effects.Impl
{
    public class ChangeBallsSpeed : Effect
    {
        [SerializeField] private float _changedBallsSpeedProgress;
        [SerializeField] private float _changeBallSpeedAnimationDuration;

        public override void Enable()
        {
            Context.BallsManager.BallsChanged += BallsManagerOnBallsChanged;
            Context.GameStatusManager.ProgressValueChanged += GameStatusManagerOnProgressValueChanged;
            _ballsSpeedCurrentProgress = Context.GameStatusManager.GetCurrentProgress();
            SetBallsSpeedProgress(_changedBallsSpeedProgress, _changeBallSpeedAnimationDuration);
        }

        public override void ForceEnable()
        {
            Context.BallsManager.BallsChanged += BallsManagerOnBallsChanged;
            Context.GameStatusManager.ProgressValueChanged += GameStatusManagerOnProgressValueChanged;
            _ballsSpeedCurrentProgress = Context.GameStatusManager.GetCurrentProgress();
            SetBallsSpeedProgress(_changedBallsSpeedProgress, 0);
        }

        public override void Disable()
        {
            Context.BallsManager.BallsChanged -= BallsManagerOnBallsChanged;
            Context.GameStatusManager.ProgressValueChanged -= GameStatusManagerOnProgressValueChanged;
            SetBallsSpeedProgress(_currentProgress, _changeBallSpeedAnimationDuration);
        }

        public override void ForceDisable()
        {
            Context.BallsManager.BallsChanged -= BallsManagerOnBallsChanged;
            Context.GameStatusManager.ProgressValueChanged -= GameStatusManagerOnProgressValueChanged;
            SetBallsSpeedProgress(_currentProgress, 0f);
        }

        private float _currentProgress;

        private void GameStatusManagerOnProgressValueChanged(float oldValue, float newValue)
        {
            _currentProgress = newValue;
            SetBallsSpeedProgress(_changedBallsSpeedProgress, _changeBallSpeedAnimationDuration);
        }

        private void BallsManagerOnBallsChanged(List<Ball> oldValue, List<Ball> newValue)
        {
            SetBallsSpeedProgress(_changedBallsSpeedProgress, _changeBallSpeedAnimationDuration);
        }

        private float _ballsSpeedCurrentProgress;

        private void SetBallsSpeedProgress(float progress, float duration)
        {
            if (duration != 0f)
            {
                DOTween.To(() => _ballsSpeedCurrentProgress,
                    x =>
                    {
                        _ballsSpeedCurrentProgress = x;
                        Context.BallsManager.SetCurrentSpeedProgress(x);
                    }, progress, duration);
            }
            else
            {
                    _ballsSpeedCurrentProgress = progress;
                    Context.BallsManager.SetCurrentSpeedProgress(progress);
            }
        }
    }
}