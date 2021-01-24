using UnityEngine;

namespace Scenes.Game.Player
{
    public class HpPopUpView : MonoBehaviour
    {
        private int _health;
        public void SetHealth(int value)
        {
            _health = value;
            Debug.Log(_health);
        }

        public void AddHealth(int value)
        {
            _health += value;
            Debug.Log(_health);
        }
    }
}