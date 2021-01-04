using UnityEngine;

namespace Scripts.Scenes.Game.Paddles
{
    public class PaddleView : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public float Width => this.transform.localScale.x * _collider.size.x;
        public float Height => this.transform.localScale.y * _collider.size.y;
    }
}
