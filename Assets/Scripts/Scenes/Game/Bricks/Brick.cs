namespace Scripts.Scenes.Game.Bricks
{
    using Scripts.Scenes.Game.Utils;
    using UnityEngine;
    public class Brick : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private BrickType Type;

        public event OnIntValueChanged OnHealthValueChanged;

        public int Health
        {
            get => _health;
            set
            {
                OnHealthValueChanged?.Invoke(this, _health, value);
                _health = value;
            }
        }

        public Vector3 Size
        {
            get => this.transform.localScale;
            set
            {
                this.transform.localScale = value;
            }
        }
    }
}
