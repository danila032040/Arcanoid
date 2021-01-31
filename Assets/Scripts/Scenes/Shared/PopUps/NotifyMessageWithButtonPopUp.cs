using System.Collections;
using DG.Tweening;
using Localization.LocalizedText;
using PopUpSystems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scenes.Shared.PopUps
{
    [RequireComponent(typeof(CanvasGroup))]
    public class NotifyMessageWithButtonPopUp : PopUp
    {
        private CanvasGroup _canvasGroup;

        [SerializeField] private float _animationDuration;
        
        [SerializeField] private Button _button;
        [SerializeField] private LocalizedTextTmpUGui _messageText;
        [SerializeField] private LocalizedTextTmpUGui _buttonText;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void EnableInput()
        {
            _canvasGroup.interactable = true;
        }

        public override void DisableInput()
        {
            _canvasGroup.interactable = false;
        }

        public void Show(string messageTextKey, string buttonTextKey, UnityAction action)
        {
            _messageText.SetKey(messageTextKey);
            _buttonText.SetKey(buttonTextKey);
            _button.onClick.AddListener(action);
            
            StartCoroutine(ShowCoroutine());
        }

        public void Hide()
        {
            _button.onClick.RemoveAllListeners();
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            _canvasGroup.alpha = 0f;
            DisableInput();
            yield return _canvasGroup.DOFade(1f, _animationDuration).WaitForCompletion();
            EnableInput();
        }
        
        private IEnumerator HideCoroutine()
        {
            yield return _canvasGroup.DOFade(0f, _animationDuration).WaitForCompletion();
            OnClosing();
        }
    }
}