using System;
using System.Collections;
using System.Collections.Generic;
using Context;
using DG.Tweening;
using PopUpSystems;
using Scenes.Game.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    //TODO:
    public class WinGamePopUp : PopUp
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _buttonNextLevel;
        [SerializeField] private Button _buttonChoosePack;

        [SerializeField] private Image _packImage;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _levelNameText;

        [SerializeField] private float _openAnimationDuration;

        public event Action ButtonNextLevelPressed;
        public event Action ButtonChoosePackPressed;

        private void Awake()
        {
            _buttonNextLevel.onClick.AddListener(OnButtonNextLevelPressed);
            _buttonChoosePack.onClick.AddListener(OnButtonChoosePackPressed);
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
            StartCoroutine(OpenAnim(gameWinInfo));
        }

        private IEnumerator OpenAnim(GameWinInfo gameWinInfo)
        {
            _canvasGroup.DOFade(0f, 0f);
            DisableInput();
            
            if (IsLastLevel(gameWinInfo))
            {
                SetButtonActivity(false, true);
            }
            else
            {
                SetButtonActivity(true, false);
            }

            yield return _canvasGroup.DOFade(1f, _openAnimationDuration).WaitForCompletion();

            if (IsLastLevel(gameWinInfo))
            {
                yield return AnimateNextLevel(gameWinInfo);
            }
            else
            {
                yield return AnimateChoosePack(gameWinInfo);
            }

            EnableInput();
        }

        private static bool IsLastLevel(GameWinInfo gameWinInfo)
        {
            return gameWinInfo._currentPack == gameWinInfo._nextPack &&
                   gameWinInfo._currentLevelNumber == gameWinInfo._nextLevelNumber;
        }

        private YieldInstruction AnimateNextLevel(GameWinInfo gameWinInfo)
        {
            return null;
        }

        private YieldInstruction AnimateChoosePack(GameWinInfo gameWinInfo)
        {
            return null;
        }

        private void SetButtonActivity(bool buttonNextLevel, bool buttonChoosePack)
        {
            _buttonNextLevel.gameObject.SetActive(buttonNextLevel);
            _buttonChoosePack.gameObject.SetActive(buttonChoosePack);
        }

        private IEnumerator CloseAnim()
        {
            _canvasGroup.DOFade(1f, 0f);
            yield return _canvasGroup.DOFade(0f, _openAnimationDuration).WaitForCompletion();
            OnClosing();
        }

        protected virtual void OnButtonNextLevelPressed()
        {
            ButtonNextLevelPressed?.Invoke();
            StartCoroutine(CloseAnim());
        }

        protected virtual void OnButtonChoosePackPressed()
        {
            ButtonChoosePackPressed?.Invoke();
            StartCoroutine(CloseAnim());
        }
    }
}