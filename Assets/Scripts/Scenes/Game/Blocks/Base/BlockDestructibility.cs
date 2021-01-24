using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks.Base
{
    public class BlockDestructibility : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        
        private int _health;
        
        public event OnIntValueChanged HealthValueChanged;

        private void Awake()
        {
            InitValues();
        }

        public void InitValues()
        {
            _health = _maxHealth;
        }

        public void AddHealth(int health)
        {
            _health += health;
            OnHealthValueChanged(_health - health, _health);
            
        }

        public float GetHealthPercentage() => _health * 1f / _maxHealth;

        private void OnHealthValueChanged(int oldvalue, int newvalue)
        {
            HealthValueChanged?.Invoke(this, oldvalue, newvalue);
        }
    }
}