using System;
using System.Collections;
using DG.Tweening;
using EnergySystem;
using PopUpSystems;
using SceneLoader;
using Scenes.ChoosePack.PopUps;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    public class RestartGamePopUp : PopUp
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _buttonRestart;
        
        [SerializeField] private float _animationDuration;

        public event Action ButtonRestartPressed;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(OnButtonRestartPressed);
        }
        

        public override void EnableInput()
        {
            _canvasGroup.interactable = true;
        }

        public override void DisableInput()
        {
            _canvasGroup.interactable = false;
        }
        
        public void ShowAnim()
        {
            StartCoroutine(OpenAnim());
        }
        
        private IEnumerator OpenAnim()
        {
            _canvasGroup.DOFade(0f, 0f);
            DisableInput();
            yield return _canvasGroup.DOFade(1f, _animationDuration).WaitForCompletion();
            EnableInput();
        }

        private IEnumerator CloseAnim()
        {
            _canvasGroup.DOFade(1f, 0f);
            yield return _canvasGroup.DOFade(0f, _animationDuration).WaitForCompletion();
            OnClosing();
        }

        private void OnButtonRestartPressed()
        {
            if (EnergyManager.Instance.CanPlayLevel())
            {
                ButtonRestartPressed?.Invoke();
                StartCoroutine(CloseAnim());
            }
            else
            {
                var popUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<NotEnoughEnergyPointsPopUp>();
                popUp.SetButtonText("Choose Pack");
                popUp.ShowAnim();
                popUp.ButtonOkPressed += () =>
                {
                    SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
                };
            }
        }
    }
}