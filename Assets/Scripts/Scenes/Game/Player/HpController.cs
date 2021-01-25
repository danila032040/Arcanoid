using Scenes.Game.Managers;
using Scenes.Game.PopUps;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Player
{
    public class HpController : MonoBehaviour
    {
        private HpPopUp _view;
        private HpModel _model;

        [SerializeField] private PopUpsManager _popUpsManager;

        private void Awake()
        {
            _view = _popUpsManager.GetHpPopUp();
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