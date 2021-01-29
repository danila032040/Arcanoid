using System.Collections.Generic;
using System.Linq;
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
        }

        public override void Disable()
        {
            Context.BallsManager.BallsChanged -= BallsManagerOnBallsChanged;
            Context.GameStatusManager.ProgressValueChanged -= GameStatusManagerOnProgressValueChanged;
            SetBallsSpeedProgress(_currentProgress);
        }

        private float _currentProgress;

        private void GameStatusManagerOnProgressValueChanged(float oldValue, float newValue)
        {
            _currentProgress = newValue;
            SetBallsSpeedProgress(_changedBallsSpeedProgress);
        }

        private void BallsManagerOnBallsChanged(List<Ball> oldValue, List<Ball> newValue)
        {
            SetBallsSpeedProgress(_changedBallsSpeedProgress);
        }

        private float _ballsSpeedCurrentProgress;
        private void SetBallsSpeedProgress(float progress)
        {
            float startProgress = _ballsSpeedCurrentProgress;
            DOTween.To(() => startProgress,
                x =>
                {
                    _ballsSpeedCurrentProgress = x;
                    Context.BallsManager.SetCurrentSpeedProgress(x);
                    Debug.Log(x);
                }, progress, _changeBallSpeedAnimationDuration);
        }
    }
}