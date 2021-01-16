using Scripts.Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Bricks
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private BrickType _type;

        public event OnIntValueChanged OnHealthValueChanged;


        public int Health
        {
            get => _health;
            set
            {
                OnHealthValueChanged?.Invoke(this, _health, value);
                _health = value;
                if (value <=0) Destroy(this.gameObject);
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
