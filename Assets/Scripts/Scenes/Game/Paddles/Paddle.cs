using UnityEngine;

namespace Scenes.Game.Paddles
{
    [RequireComponent(typeof(PaddleMovement))]
    [RequireComponent(typeof(PaddleView))]
    public class Paddle : MonoBehaviour
    {
        private PaddleMovement _paddleMovement;
        private PaddleView _paddleView;

        public void Awake()
        {
            _paddleMovement = GetComponent<PaddleMovement>();
            _paddleView = GetComponent<PaddleView>();
        }

        public PaddleMovement GetPaddleMovement() => _paddleMovement;
        public PaddleView GetPaddleView() => _paddleView;
    }
}