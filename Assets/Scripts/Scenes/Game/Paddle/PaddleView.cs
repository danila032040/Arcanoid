using UnityEngine;

namespace Scripts.Scenes.Game.Paddle
{
    public class PaddleView : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public float Width => _collider.size.x;
        public float Height => _collider.size.y;
    }
}
