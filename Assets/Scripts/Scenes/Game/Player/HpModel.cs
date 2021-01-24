using Scenes.Game.Utils;

namespace Scenes.Game.Player
{
    public class HpModel
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

        public int GetHpValue() => _health;
    }
}