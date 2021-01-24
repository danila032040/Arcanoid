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
            Ball ball = _ballsPool.Get();
            _balls.Add(ball);
            OnBallsChanged(_balls);
            return ball;
        }

        public void RemoveBall(Ball ball)
        {
            _ballsPool.Remove(ball);
            _balls.Remove(ball);
            OnBallsChanged(_balls);
        }

        public List<Ball> GetBalls() => _balls;

        private void OnBallsChanged(List<Ball> obj)
        {
            BallsChanged?.Invoke(obj);
        }
    }
}