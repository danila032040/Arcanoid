using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scenes.Game.Balls.Base;
using Scenes.Game.Balls.Pool;
using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallsManager : MonoBehaviour
    {
        [SerializeField] private BallsPool _ballsPool;

        private readonly List<Ball> _balls = new List<Ball>();

        public event Action<List<Ball>> BallsChanged;


        public Ball SpawnBall()
        {
            Ball ball = SpawnOneBall();
            OnBallsChanged(_balls);
            return ball;
        }

        public void RemoveBall(Ball ball)
        {
            RemoveOneBall(ball);
            OnBallsChanged(_balls);
        }

        public int GetBallsCount() => _balls.Count;

        private void OnBallsChanged(List<Ball> obj)
        {
            BallsChanged?.Invoke(obj);
        }

        public void DeleteBalls()
        {
            while (_balls.Count > 0)
            {
                RemoveOneBall(_balls[0]);
            }

            OnBallsChanged(_balls);
        }

        private Ball SpawnOneBall()
        {
            Ball ball = _ballsPool.Get();
            _balls.Add(ball);
            return ball;
        }

        private void RemoveOneBall(Ball ball)
        {
            _ballsPool.Remove(ball);
            _balls.Remove(ball);
        }

        private Coroutine _angryBallCoroutine;

        public void AngryBall(float duration)
        {
            if (_angryBallCoroutine != null)
            {
                StopCoroutine(_angryBallCoroutine);
                _angryBallCoroutine = null;
            }

            _angryBallCoroutine = StartCoroutine(AngryBallCoroutine(duration));
        }

        private IEnumerator AngryBallCoroutine(float duration)
        {
            SetAllBallsAngry(true)(_balls);

            Action<List<Ball>> ballsChangedAction = SetAllBallsAngry(true);

            BallsChanged += ballsChangedAction;
            yield return new WaitForSeconds(duration);
            BallsChanged -= ballsChangedAction;

            SetAllBallsAngry(false)(_balls);
        }

        private Action<List<Ball>> SetAllBallsAngry(bool value)
        {
            return list =>
            {
                foreach (Ball ball in list)
                {
                    ball.SetAngryBall(value);
                }
            };
        }

        [SerializeField] private float _increasedSpeedProgress;
        [SerializeField] private float _decreaseSpeedProgress;

        private float _currentSpeedProgress;

        private Coroutine _incDecSpeedCoroutine;

        public void IncreaseSpeed(float effectDuration)
        {
            if (_incDecSpeedCoroutine != null)
            {
                StopCoroutine(_incDecSpeedCoroutine);
                _incDecSpeedCoroutine = null;
            }

            _incDecSpeedCoroutine = StartCoroutine(ChangeSpeedForDuration(_increasedSpeedProgress, effectDuration));
        }

        public void DecreaseSpeed(float effectDuration)
        {
            if (_incDecSpeedCoroutine != null)
            {
                StopCoroutine(_incDecSpeedCoroutine);
                _incDecSpeedCoroutine = null;
            }

            _incDecSpeedCoroutine = StartCoroutine(ChangeSpeedForDuration(_decreaseSpeedProgress, effectDuration));
        }

        [SerializeField] private float _changeSpeedAnimationDuration;

        private IEnumerator ChangeSpeedForDuration(float speed, float effectDuration)
        {
            yield return DOTween.To(() => _currentSpeedProgress, x =>
                {
                    _currentSpeedProgress = x;
                    ChangeBallsSpeedProgress(x);
                }, speed, _changeSpeedAnimationDuration)
                .WaitForCompletion();

            yield return new WaitForSeconds(effectDuration);

            yield return DOTween.To(() => _currentSpeedProgress, x =>
                {
                    _currentSpeedProgress = x;
                    ChangeBallsSpeedProgress(x);
                }, _newSpeedProgress, _changeSpeedAnimationDuration)
                .WaitForCompletion();
        }

        private float _newSpeedProgress;

        public void SetCurrentSpeedProgress(float speedProgress)
        {
            if (_incDecSpeedCoroutine == null)
            {
                _currentSpeedProgress = speedProgress;
            }

            _newSpeedProgress = speedProgress;

            ChangeBallsSpeedProgress(_currentSpeedProgress);
        }

        private void ChangeBallsSpeedProgress(float speedProgress)
        {
            foreach (Ball ball in _balls)
            {
                ball.GetBallMovement().SetCurrentSpeedProgress(speedProgress);
            }
        }
    }
}