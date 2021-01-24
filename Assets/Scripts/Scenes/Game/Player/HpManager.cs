using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Player
{
    public class HpManager : MonoBehaviour
    {
        private int _health;

        public event OnIntValueChanged HealthValueChanged;

        public void AddHealth(int value)
        {
            _health += value;
            OnHealthValueChanged(_health - value, _health);
        }

        public void SetHealth(int value)
        {
            int oldValue = _health;
            _health = value;
            OnHealthValueChanged(oldValue, value);
        }

        private void OnHealthValueChanged(int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(this, oldValue, newValue);
        }
    }
}