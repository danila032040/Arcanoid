using System.Collections.Generic;
using Scenes.Game.Balls.Base;
using Scenes.Game.Balls.Pool;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallsManager : MonoBehaviour
    {
        [SerializeField] private BallsPool _ballsPool;

        private readonly List<Ball> _balls = new List<Ball>();

        public event OnValueChanged<List<Ball>> BallsChanged;


        public Ball SpawnBall()
        {
            List<Ball> oldValue = new List<Ball>(_balls);
            Ball ball = SpawnOneBall();
            OnBallsChanged(oldValue, _balls);
            return ball;
        }

        public void RemoveBall(Ball ball)
        {
            List<Ball> oldValue = new List<Ball>(_balls);
            RemoveOneBall(ball);
            OnBallsChanged(oldValue, _balls);
        }

        public List<Ball> GetBalls() => _balls;
        public int GetBallsCount() => _balls.Count;

        

        public void DeleteBalls()
        {
            List<Ball> oldValue = new List<Ball>(_balls);
            while (_balls.Count > 0)
            {
                RemoveOneBall(_balls[0]);
            }

            OnBallsChanged(oldValue, _balls);
        }

        private void OnBallsChanged(List<Ball> oldValue, List<Ball> newValue)
        {
            BallsChanged?.Invoke(oldValue, newValue);
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
        
        public void SetCurrentSpeedProgress(float speedProgress)
        {
            ChangeBallsSpeedProgress(speedProgress);
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