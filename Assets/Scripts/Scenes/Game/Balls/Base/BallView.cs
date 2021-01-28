using UnityEngine;

namespace Scenes.Game.Balls.Base
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _angryColor;
        [SerializeField] private Color _normalColor;

        public void SetAngryBall(bool value)
        {
            _spriteRenderer.color = value ? _angryColor : _normalColor;
        }
    }
}