using System;
using System.Collections;
using Context;
using DG.Tweening;
using EnergySystem;
using PopUpSystems;
using SceneLoader;
using Scenes.Game.Managers;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Scenes.Shared.PopUps
{
    public class PauseGamePopUp : PopUp
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonReturn;

        [SerializeField] private float _animationDuration;
        [SerializeField] private float _pauseAnimationDuration;

        public override void EnableInput()
        {
            _canvasGroup.interactable = true;
        }

        public override void DisableInput()
        {
            _canvasGroup.interactable = false;
        }

        private GameManager _gameManager;
        public void Show(GameManager gameManager, bool stopTime)
        {
            _gameManager = gameManager;
            _buttonContinue.onClick.AddListener(OnButtonContinuePressed);
            _buttonRestart.onClick.AddListener(OnButtonRestartPressed);
            _buttonReturn.onClick.AddListener(OnButtonReturnPressed);
            
            StartCoroutine(ShowCoroutine(stopTime));
        }
        private void Hide(bool returnTime)
        {
            _buttonContinue.onClick.RemoveListener(OnButtonContinuePressed);
            _buttonRestart.onClick.RemoveListener(OnButtonRestartPressed);
            _buttonReturn.onClick.RemoveListener(OnButtonReturnPressed);
            StartCoroutine(HideCoroutine(returnTime));
        }

        private IEnumerator ShowCoroutine(bool stopTime)
        {
            _canvasGroup.alpha = 0f;
            DisableInput();
            if (stopTime) yield return DOTween.To(() => Time.timeScale, scale => Time.timeScale = scale, 0f, _pauseAnimationDuration).WaitForCompletion();
            yield return _canvasGroup.DOFade(1f, _animationDuration).WaitForCompletion();
            EnableInput();
        }

        private IEnumerator HideCoroutine(bool returnTime)
        {
            _canvasGroup.alpha = 1f;
            yield return _canvasGroup.DOFade(0f, _animationDuration).WaitForCompletion();
            OnClosing();
            if (returnTime) DOTween.To(() => Time.timeScale, scale => Time.timeScale = scale, 1f, _pauseAnimationDuration);
        }

        private void OnButtonContinuePressed()
        {
            Hide(true);
        }

        private void OnButtonRestartPressed()
        {
            if (EnergyManager.Instance.CanPlayLevel())
            {
                _gameManager.GameRestart();
                Hide(true);
            }
            else
            {
                NotifyMessageWithButtonPopUp popUp =
                    PopUpSystem.Instance.SpawnPopUpOnANewLayer<NotifyMessageWithButtonPopUp>();
                popUp.Show(
                    ProjectContext.Instance.NotifyPopUpLocalizationConstants.NotEnoughEnergy,
                    ProjectContext.Instance.NotifyPopUpLocalizationConstants.Ok, popUp.Hide);
            }
        }

        private void OnButtonReturnPressed()
        {
            SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
            Hide(false);
        }
    }
}