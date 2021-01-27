using System;
using PopUpSystems;
using Scenes.Game.PopUps.MainGamePopUps;
using Scenes.Game.PopUps.PauseGamePopUps;
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

        public void OnDestroy()
        {
            _mainGamePopUp.Close();
        }

        public MainGamePopUp GetMainGamePopUp() => _mainGamePopUp;

        public void GamePause()
        {
            OnPauseGame();
            PauseGamePopUp pauseGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<PauseGamePopUp>();

            pauseGamePopUp.Open();
            
            pauseGamePopUp.ButtonContinuePressed += PauseGamePopUpOnButtonContinuePressed;
            pauseGamePopUp.ButtonRestartPressed += PauseGamePopUpOnButtonRestartPressed;
            pauseGamePopUp.ButtonReturnPressed += PauseGamePopUpOnButtonReturnPressed;

            pauseGamePopUp.Closing += (popUp) =>
            {
                pauseGamePopUp.ButtonContinuePressed -= PauseGamePopUpOnButtonContinuePressed;
                pauseGamePopUp.ButtonRestartPressed -= PauseGamePopUpOnButtonRestartPressed;
                pauseGamePopUp.ButtonReturnPressed -= PauseGamePopUpOnButtonReturnPressed;
            };


        }
        private void PauseGamePopUpOnButtonContinuePressed()
        {
            OnUnPauseGame();
        }
        private void PauseGamePopUpOnButtonRestartPressed()
        {
            OnUnPauseGame();
            OnRestartGame();
        }
        private void PauseGamePopUpOnButtonReturnPressed()
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