using UnityEngine;

namespace Scenes.Game.Balls
{
    [RequireComponent(typeof(BallMovement))]
    [RequireComponent(typeof(BallAttachment))]
    [RequireComponent(typeof(BallCollision))]
    public class Ball : MonoBehaviour
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