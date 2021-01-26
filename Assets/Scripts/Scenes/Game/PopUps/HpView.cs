using PopUpSystems;
using TMPro;
using UnityEngine;

namespace Scenes.Game.PopUps
{
    public class HpView : MonoBehaviour
    {
        private int _health;

        [SerializeField] private TextMeshProUGUI _textHealthCount;
        
        public void SetHealth(int value)
        {
            _health = value;
            _textHealthCount.text = _health.ToString();
        }

        public void AddHealth(int value)
        {
            _health += value;
            _textHealthCount.text = _health.ToString();
        }
    }
}