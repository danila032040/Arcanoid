using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private BlockType _type;

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
            set => this.transform.localScale = value;
        }
    }
}
