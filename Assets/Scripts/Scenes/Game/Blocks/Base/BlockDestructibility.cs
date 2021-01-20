using System;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Blocks
{
    public class BlockDestructibility : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        
        private int _health;
        
        public event OnIntValueChanged OnHealthValueChanged;

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
            OnHealthValueChanged?.Invoke(this, _health - health, _health);
            
        }

        public float GetHealthPercentage() => _health * 1f / _maxHealth;
    }
}