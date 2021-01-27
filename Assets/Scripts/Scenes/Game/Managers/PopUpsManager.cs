using System;
using Context;
using PopUpSystems;
using Scenes.Game.PopUps;
using Scenes.Game.PopUps.MainGamePopUps;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PopUpsManager : MonoBehaviour
    {
        public event Action PauseGame;
        public event Action UnPauseGame;
        public event Action RestartGame;
        public event Action ReturnGame;

        private MainGamePopUp _mainGamePopUp;

        private void Awake()
        {
            _mainGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<MainGamePopUp>();

            _mainGamePopUp.ButtonPauseGamePressed += GamePause;
        }

        public MainGamePopUp GetMainGamePopUp() => _mainGamePopUp;

        public void GamePause()
        {
            OnPauseGame();
            PauseGamePopUp pauseGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<PauseGamePopUp>();

            pauseGamePopUp.Open();

            pauseGamePopUp.ButtonContinuePressed += UpOnButtonContinuePressed;
            pauseGamePopUp.ButtonRestartPressed += OnButtonRestartPressed;
            pauseGamePopUp.ButtonReturnPressed += OnButtonReturnPressed;

            pauseGamePopUp.Closing += (popUp) =>
            {
                pauseGamePopUp.ButtonContinuePressed -= UpOnButtonContinuePressed;
                pauseGamePopUp.ButtonRestartPressed -= OnButtonRestartPressed;
                pauseGamePopUp.ButtonReturnPressed -= OnButtonReturnPressed;
            };
        }

        public void GameOver()
        {
            OnPauseGame();
            RestartGamePopUp restartGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<RestartGamePopUp>();

            restartGamePopUp.Open();
            
            restartGamePopUp.ButtonRestartPressed += OnButtonRestartPressed;
            restartGamePopUp.Closing += (popUp) =>
            {
                restartGamePopUp.ButtonRestartPressed -= OnButtonRestartPressed;
            };
        }
        
        //TODO:
        public void GameWin(GameWinInfo gameWinInfo)
        {
        }

        private void UpOnButtonContinuePressed()
        {
            OnUnPauseGame();
        }

        private void OnButtonRestartPressed()
        {
            OnUnPauseGame();
            OnRestartGame();
        }

        private void OnButtonReturnPressed()
        {
            OnUnPauseGame();
            OnReturnGame();
        }

        protected virtual void OnPauseGame()
        {
            PauseGame?.Invoke();
        }

        protected virtual void OnUnPauseGame()
        {
            UnPauseGame?.Invoke();
        }

        protected virtual void OnRestartGame()
        {
            RestartGame?.Invoke();
        }

        protected virtual void OnReturnGame()
        {
            ReturnGame?.Invoke();
        }

       
    }
}