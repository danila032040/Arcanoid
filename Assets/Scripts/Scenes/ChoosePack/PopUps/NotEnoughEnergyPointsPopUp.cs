using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PopUpSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ChoosePack.PopUps
{
    public class NotEnoughEnergyPointsPopUp : PopUp
    {
        [SerializeField] private Button _buttonOk;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _animationDuration;

        private void Awake()
        {
            _buttonOk.onClick.AddListener(() => StartCoroutine(CloseAnim()));
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
    }
}