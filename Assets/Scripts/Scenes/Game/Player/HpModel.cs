using Scenes.Game.Utils;
using Unity.Mathematics;

namespace Scenes.Game.Player
{
    public class HpModel
    {
        private int _health;

        public event OnValueChanged<int> HealthValueChanged;

        public void AddHealth(int value)
        {
            _health += value;
            _health = math.max(_health, 0);
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
            HealthValueChanged?.Invoke(oldValue, newValue);
        }

        public int GetHpValue() => _health;
    }
}