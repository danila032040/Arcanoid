using System;
using PopUpSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    public class PauseMenuButtonPopUp : PopUp
    {
        [SerializeField] private Button _button;

        private EventSystem _eventSystem;

        public event Action ButtonPausePressed;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonPausePressed);
        }

        private void OnButtonPausePressed()
        {
            ButtonPausePressed?.Invoke();
        }

        public override void DisableInput()
        {
            _button.interactable = false;
        }

        public override void EnableInput()
        {
            _button.interactable = true;
        }

        
    }
}