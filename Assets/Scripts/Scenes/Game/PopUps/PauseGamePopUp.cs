using System;
using System.Collections;
using DG.Tweening;
using PopUpSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    public class PauseGamePopUp : PopUp
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonReturn;
        
        [SerializeField] private float _animationDuration;

        public event Action ButtonContinuePressed;
        public event Action ButtonRestartPressed;
        public event Action ButtonReturnPressed;

        private void Awake()
        {
            _buttonContinue.onClick.AddListener(OnButtonContinuePressed);
            _buttonRestart.onClick.AddListener(OnButtonRestartPressed);
            _buttonReturn.onClick.AddListener(OnButtonReturnPressed);
        }
        

        public override void EnableInput()
        {
            _canvasGroup.interactable = true;
        }

        public override void DisableInput()
        {
            _canvasGroup.interactable = false;
        }
        
        public void Open()
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

        private void OnButtonContinuePressed()
        {
            ButtonContinuePressed?.Invoke();
            StartCoroutine(CloseAnim());
        }

        private void OnButtonRestartPressed()
        {
            ButtonRestartPressed?.Invoke();
            StartCoroutine(CloseAnim());
        }

        private void OnButtonReturnPressed()
        {
            ButtonReturnPressed?.Invoke();
            StartCoroutine(CloseAnim());
        }

        
    }
}