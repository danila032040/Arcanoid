using System;
using PopUpSystems;
using Scenes.Game.Player;
using Scenes.Shared.PopUps.MainGamePopUpViews;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Shared.PopUps
{
    public class MainGamePopUp : PopUp
    {
        public event Action ButtonPauseGamePressed;

        [SerializeField] private Button _buttonPauseGame;

        [SerializeField] private HpView _hpView;
        [SerializeField] private ProgressGameView _progressGameView;
        [SerializeField] private PackGameView _packGameView;

        public override void EnableInput() => _buttonPauseGame.interactable = true;
        public override void DisableInput() => _buttonPauseGame.interactable = false;

        public HpView GetHpView() => _hpView;
        public ProgressGameView GetProgressGameView() => _progressGameView;
        public PackGameView GetPackGameView() => _packGameView;

        private void Awake()
        {
            _buttonPauseGame.onClick.AddListener(OnButtonPauseGamePressed);
        }

        private void OnButtonPauseGamePressed()
        {
            ButtonPauseGamePressed?.Invoke();
        }

        public void Close()
        {
            OnClosing();
        }
    }
}