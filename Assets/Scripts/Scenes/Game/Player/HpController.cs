using Scenes.Game.Managers;
using Scenes.Game.PopUps;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Player
{
    public class HpController : MonoBehaviour
    {
        private HpView _view;
        private HpModel _model;

        [SerializeField] private PopUpsManager _popUpsManager;

        private void Awake()
        {
            _view = _popUpsManager.GetMainGamePopUp().GetHpView();
            _model = new HpModel();
        }

        public int GetHpValue() => _model.GetHpValue();

        public void SetHpValue(int value)
        {
            int oldValue = _model.GetHpValue();
            _view.SetHealth(value);
            _model.SetHealth(value);
            OnHealthValueChanged(_model, oldValue, _model.GetHpValue());
        }

        public void AddHpValue(int value)
        {
            int oldValue = _model.GetHpValue();
            _view.AddHealth(value);
            _model.AddHealth(value);
            OnHealthValueChanged(_model, oldValue, _model.GetHpValue());
        }

        public event OnIntValueChanged HealthValueChanged;

        private void OnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(sender, oldValue, newValue);
        }
    }
}