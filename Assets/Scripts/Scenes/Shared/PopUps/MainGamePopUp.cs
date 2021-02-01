using DG.Tweening;
using PopUpSystems;
using Scenes.Game.Managers;
using Scenes.Game.Player;
using Scenes.Shared.PopUps.MainGamePopUpViews;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Shared.PopUps
{
    public class MainGamePopUp : PopUp
    {
        [SerializeField] private Button _buttonPauseGame;

        [SerializeField] private HpView _hpView;
        [SerializeField] private ProgressGameView _progressGameView;
        [SerializeField] private PackGameView _packGameView;

        public override void EnableInput()
        {
            if (_buttonPauseGame)
                _buttonPauseGame.interactable = true;
        }

        public override void DisableInput()
        {
            if (_buttonPauseGame)
                _buttonPauseGame.interactable = false;
        }

        public HpView GetHpView() => _hpView;
        public ProgressGameView GetProgressGameView() => _progressGameView;
        public PackGameView GetPackGameView() => _packGameView;

        private void Awake()
        {
            _buttonPauseGame.onClick.AddListener(OnButtonPauseGamePressed);
        }

        private GameManager _gameManager;

        public void Show(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnButtonPauseGamePressed()
        {
            _buttonPauseGame.onClick.RemoveListener(OnButtonPauseGamePressed);
            PauseGamePopUp popUp = PopUpSystem.Instance.SpawnPopUpOnANewLayer<PauseGamePopUp>();
            popUp.Show(_gameManager, true);
            popUp.Closing += PauseGamePopUpOnClosing;
        }

        private void PauseGamePopUpOnClosing(PopUp obj)
        {
            _buttonPauseGame.onClick.AddListener(OnButtonPauseGamePressed);
            obj.Closing -= PauseGamePopUpOnClosing;
        }

        public void Close()
        {
            OnClosing();
        }
    }
}