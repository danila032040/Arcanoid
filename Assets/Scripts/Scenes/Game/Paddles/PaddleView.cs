using Scenes.Game.Balls.Base;
using UnityEngine;

namespace Scenes.Game.Paddles
{
    public class PaddleView : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _initialScale;
        
        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        public float Width => transform.localScale.x * _collider.size.x;
        public float Height => transform.localScale.y * _collider.size.y;

        public Vector3 GetInitialScale() => _initialScale;
        public Vector3 GetCurrentScale() => transform.localScale;

        public void SetScale(Vector3 scale)
        {
            BallAttachment ballAttachment = GetComponentInChildren<BallAttachment>();
            ballAttachment?.Detach();
            transform.localScale = scale;
            ballAttachment?.AttachTo(transform);
        }
    }
}