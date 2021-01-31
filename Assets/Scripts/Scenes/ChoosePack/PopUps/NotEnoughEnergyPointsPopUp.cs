using System;
using System.Collections;
using DG.Tweening;
using PopUpSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ChoosePack.PopUps
{
    public class NotEnoughEnergyPointsPopUp : PopUp
    {
        [SerializeField] private Button _buttonOk;
        [SerializeField] private TextMeshProUGUI _buttonOkText;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _animationDuration;

        public event Action ButtonOkPressed;

        private void Awake()
        {
            _buttonOk.onClick.AddListener(OnButtonOkPressed);
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
            DisableInput();
            yield return _canvasGroup.DOFade(1f, _animationDuration).WaitForCompletion();
            EnableInput();
        }
        
        private IEnumerator CloseAnim()
        {
            yield return _canvasGroup.DOFade(0f, _animationDuration).WaitForCompletion();
            OnClosing();
        }

        private void OnButtonOkPressed()
        {
            ButtonOkPressed?.Invoke();
            StartCoroutine(CloseAnim());
        }

        public void SetButtonText(string text)
        {
            _buttonOkText.text = text;
        }
    }
}