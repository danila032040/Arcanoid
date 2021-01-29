using UnityEngine;

namespace Scenes.Game.Balls.Base
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;
    }
}