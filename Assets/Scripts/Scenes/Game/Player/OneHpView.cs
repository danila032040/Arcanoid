using Pool.Interfaces;
using UnityEngine;

namespace Scenes.Game.Player
{
    public class OneHpView : MonoBehaviour, IPoolable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;
    }
}