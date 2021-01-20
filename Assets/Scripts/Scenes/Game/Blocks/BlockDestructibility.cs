using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    public class BlockDestructibility : MonoBehaviour
    {
        [SerializeField] private int _health;
        
        public event OnIntValueChanged OnHealthValueChanged;

        public void AddHealth(int health)
        {
            _health += health;
            OnHealthValueChanged?.Invoke(this, _health - health, _health);
        }
    }
}