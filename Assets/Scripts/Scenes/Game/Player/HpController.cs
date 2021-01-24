using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Player
{
    [RequireComponent(typeof(HpPopUpView))]
    public class HpController : MonoBehaviour
    {
        private HpPopUpView _view;
        private HpModel _model;

        private void Awake()
        {
            _view = GetComponent<HpPopUpView>();
            _model = new HpModel();
            _model.HealthValueChanged += OnHealthValueChanged;
        }

        public int GetHpValue() => _model.GetHpValue();

        public void SetHpValue(int value)
        {
            _model.SetHealth(value);
            _view.SetHealth(value);
        }

        public void AddHpValue(int value)
        {
            _model.AddHealth(value);
            _view.AddHealth(value);
        }

        public event OnIntValueChanged HealthValueChanged;

        private void OnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(sender, oldValue, newValue);
        }
    }
}