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

        public event Action NextLevelGame;

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

            pauseGamePopUp.ShowAnim();

            pauseGamePopUp.ButtonContinuePressed += OnButtonContinuePressed;
            pauseGamePopUp.ButtonRestartPressed += OnButtonRestartPressed;
            pauseGamePopUp.ButtonReturnPressed += OnButtonReturnPressed;

            pauseGamePopUp.Closing += (popUp) =>
            {
                pauseGamePopUp.ButtonContinuePressed -= OnButtonContinuePressed;
                pauseGamePopUp.ButtonRestartPressed -= OnButtonRestartPressed;
                pauseGamePopUp.ButtonReturnPressed -= OnButtonReturnPressed;
            };
        }

        public void GameOver()
        {
            OnPauseGame();
            RestartGamePopUp restartGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<RestartGamePopUp>();

            restartGamePopUp.ShowAnim();
            
            restartGamePopUp.ButtonRestartPressed += OnButtonRestartPressed;
            restartGamePopUp.Closing += (popUp) =>
            {
                restartGamePopUp.ButtonRestartPressed -= OnButtonRestartPressed;
            };
        }
        
        //TODO:
        public void GameWin(GameWinInfo gameWinInfo)
        {
            OnPauseGame();

            WinGamePopUp winGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<WinGamePopUp>();

            winGamePopUp.ShowAnim(gameWinInfo);

            winGamePopUp.ButtonNextLevelPressed += OnButtonNextLevelPressed;
            winGamePopUp.ButtonChoosePackPressed += OnButtonReturnPressed;
            winGamePopUp.Closing += (popUp) =>
            {
                winGamePopUp.ButtonNextLevelPressed -= OnButtonNextLevelPressed;
            };
        }

        

        private void OnButtonContinuePressed()
        {
            OnUnPauseGame();
        }
        
        private void OnButtonNextLevelPressed()
        {
            OnUnPauseGame();
            OnNextLevelGame();
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

        private void OnPauseGame()
        {
            PauseGame?.Invoke();
        }

        private void OnUnPauseGame()
        {
            UnPauseGame?.Invoke();
        }

        private void OnRestartGame()
        {
            RestartGame?.Invoke();
        }

        private void OnReturnGame()
        {
            ReturnGame?.Invoke();
        }


        private void OnNextLevelGame()
        {
            NextLevelGame?.Invoke();
        }

    }
}