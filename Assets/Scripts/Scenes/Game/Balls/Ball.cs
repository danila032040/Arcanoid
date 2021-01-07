using UnityEngine;

namespace Scenes.Game.Balls
{
    [RequireComponent(typeof(BallMovement))]
    [RequireComponent(typeof(BallAttachment))]
    public class Ball : MonoBehaviour
    {
        private BallMovement _ballMovement;
        private BallAttachment _ballAttachment;

        private void Awake()
        {
            _ballMovement = GetComponent<BallMovement>();
            _ballAttachment = GetComponent<BallAttachment>();
        }

        public BallMovement GetBallMovement() => _ballMovement;
        public BallAttachment GetBallAttachment() => _ballAttachment;
    }
}