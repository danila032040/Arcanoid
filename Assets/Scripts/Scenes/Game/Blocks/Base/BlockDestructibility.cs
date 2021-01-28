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

        public void SetHealth(int value)
        {
            int oldValue = _health;
            _health = value;
            OnHealthValueChanged(oldValue, _health);
        }

        public float GetHealthPercentage() => _health * 1f / _maxHealth;

        private void OnHealthValueChanged(int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(this, oldValue, newValue);
        }
    }
}