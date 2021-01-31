using Context;
using Scenes.Game.Managers;
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
            if (value > ProjectContext.Instance.GetHealthConfig().InitialPlayerHealthValue) return;
            
            int oldValue = _model.GetHpValue();
            _view.SetHealth(value);
            _model.SetHealth(value);
            OnHealthValueChanged(oldValue, _model.GetHpValue());
        }

        public void AddHpValue(int value)
        {
            if (_model.GetHpValue() + value > ProjectContext.Instance.GetHealthConfig().InitialPlayerHealthValue) return;
            int oldValue = _model.GetHpValue();
            
            _view.AddHealth(value);
            _model.AddHealth(value);
            
            OnHealthValueChanged(oldValue, _model.GetHpValue());
        }

        public event OnValueChanged<int> HealthValueChanged;

        private void OnHealthValueChanged(int oldValue, int newValue)
        {
            HealthValueChanged?.Invoke(oldValue, newValue);
        }
    }
}