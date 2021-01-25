using PopUpSystems;
using TMPro;
using UnityEngine;

namespace Scenes.Game.PopUps
{
    public class HpPopUp : PopUp
    {
        private int _health;

        [SerializeField] private TextMeshProUGUI _text;
        
        public void SetHealth(int value)
        {
            _health = value;
            _text.text = _health.ToString();
        }

        public void AddHealth(int value)
        {
            _health += value;
            _text.text = _health.ToString();
        }

        public override void DisableInput()
        {
        }

        public override void EnableInput()
        {
        }
    }
}