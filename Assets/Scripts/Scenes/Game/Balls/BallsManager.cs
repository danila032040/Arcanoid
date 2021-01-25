using System;
using System.Collections.Generic;
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

        public List<Ball> GetBalls() => _balls;

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
    }
}