using Pool.Interfaces;
using UnityEngine;

namespace Scenes.Game.Balls.Base
{
    [RequireComponent(typeof(BallMovement))]
    [RequireComponent(typeof(BallAttachment))]
    [RequireComponent(typeof(BallCollision))]
    public class Ball : MonoBehaviour, IPoolable
    {
        private BallMovement _ballMovement;
        private BallAttachment _ballAttachment;
        private BallCollision _ballCollision;

        private void Awake()
        {
            _ballMovement = GetComponent<BallMovement>();
            _ballAttachment = GetComponent<BallAttachment>();
            _ballCollision = GetComponent<BallCollision>();
        }

        public BallMovement GetBallMovement() => _ballMovement;
        public BallAttachment GetBallAttachment() => _ballAttachment;
        public BallCollision GetBallCollision() => _ballCollision;
    }
}