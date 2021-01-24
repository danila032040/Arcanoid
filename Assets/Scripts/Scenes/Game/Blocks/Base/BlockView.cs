using UnityEngine;

namespace Scenes.Game.Blocks.Base
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Vector3 Size
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }

        public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;
    }
}