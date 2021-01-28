using Pool.Interfaces;
using UnityEngine;

namespace Scenes.Game.Balls.Base
{
    [RequireComponent(typeof(BallMovement))]
    [RequireComponent(typeof(BallAttachment))]
    [RequireComponent(typeof(BallCollision))]
    [RequireComponent(typeof(BallView))]
    public class Ball : MonoBehaviour, IPoolable
    {
        private BallMovement _ballMovement;
        private BallAttachment _ballAttachment;
        private BallCollision _ballCollision;
        private BallView _ballView;

        private void Awake()
        {
            _ballMovement = GetComponent<BallMovement>();
            _ballAttachment = GetComponent<BallAttachment>();
            _ballCollision = GetComponent<BallCollision>();
            _ballView = GetComponent<BallView>();
        }

        public BallMovement GetBallMovement() => _ballMovement;
        public BallAttachment GetBallAttachment() => _ballAttachment;
        public BallCollision GetBallCollision() => _ballCollision;
        public BallView GetBallView() => _ballView;

        public void SetAngryBall(bool value)
        {
            _ballCollision.SetAngryBall(value);
            _ballMovement.SetAngryBall(value);
            _ballView.SetAngryBall(value);
        }
    }
}