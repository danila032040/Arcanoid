using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PopUpSystems;
using Scenes.Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    //TODO:
    public class WinGamePopUp : PopUp
    {
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _buttonNextLevel;
        
        [SerializeField] private float _animationDuration;

        public event Action ButtonNextLevelPressed;

        private void Awake()
        {
            _buttonNextLevel.onClick.AddListener(OnButtonNextLevelPressed);
        }
        
        public override void EnableInput()
        {
            _canvasGroup.interactable = true;
        }

        public override void DisableInput()
        {
            _canvasGroup.interactable = false;
        }

        public void ShowAnim(GameWinInfo gameWinInfo)
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

        protected virtual void OnButtonNextLevelPressed()
        {
            ButtonNextLevelPressed?.Invoke();
            StartCoroutine(CloseAnim());
        }
    }
}