using UnityEngine;

namespace Scenes.Game.Paddles
{
    [RequireComponent(typeof(PaddleMovement))]
    [RequireComponent(typeof(PaddleView))]
    [RequireComponent(typeof(PaddleCollision))]
    public class Paddle : MonoBehaviour
    {
        private PaddleMovement _paddleMovement;
        private PaddleView _paddleView;
        private PaddleCollision _paddleCollision;

        public void Awake()
        {
            _paddleMovement = GetComponent<PaddleMovement>();
            _paddleView = GetComponent<PaddleView>();
            _paddleCollision = GetComponent<PaddleCollision>();
        }

        public PaddleMovement GetPaddleMovement() => _paddleMovement;
        public PaddleView GetPaddleView() => _paddleView;
        public PaddleCollision GetPaddleCollision() => _paddleCollision;
        
    }
}