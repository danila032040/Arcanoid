using System;
using System.Collections;
using DG.Tweening;
using PopUpSystems;
using Scenes.Game.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    public class WinGamePopUp : PopUp
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private GameObject _levelInfos;
        [SerializeField] private GameObject _allPacksPassed;
        [SerializeField] private GameObject _notEnoughEp;

        [SerializeField] private Button _buttonNextLevel;
        [SerializeField] private CanvasGroup _buttonNextLevelCanvasGroup;

        [SerializeField] private Button _buttonChoosePack;
        [SerializeField] private CanvasGroup _buttonChoosePackCanvasGroup;

        [SerializeField] private Image _packImage;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _levelNameText;

        [SerializeField] private float _openAnimationDuration;
        [SerializeField] private float _showButtonAnimationDuration;

        [SerializeField] private float _scaleAnimationDuration;
        [SerializeField] private float _rotationAnimationDuration;

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

            if (IsLastLevel(gameWinInfo) || !gameWinInfo._enoughEnergy)
            {
                SetButtonActivity(false, true);
            }
            else
            {
                SetButtonActivity(true, false);
            }

            SetCurrentLevel(gameWinInfo);

            yield return _canvasGroup.DOFade(1f, _openAnimationDuration).WaitForCompletion();

            if (IsLastLevel(gameWinInfo) || !gameWinInfo._enoughEnergy)
            {
                yield return StartCoroutine(AnimateChoosePack(gameWinInfo));
                yield return _buttonChoosePackCanvasGroup.DOFade(1f, _showButtonAnimationDuration);
            }
            else
            {
                yield return StartCoroutine(AnimateNextLevel(gameWinInfo));
                yield return _buttonNextLevelCanvasGroup.DOFade(1f, _showButtonAnimationDuration);
            }

            EnableInput();
        }

        private static bool IsLastLevel(GameWinInfo gameWinInfo)
        {
            return gameWinInfo._currentPack == gameWinInfo._nextPack &&
                   gameWinInfo._currentLevelNumber == gameWinInfo._nextLevelNumber;
        }


        private void SetCurrentLevel(GameWinInfo gameWinInfo)
        {
            _packImage.sprite = gameWinInfo._currentPack.GetPackSprite();
            _packNameText.text = gameWinInfo._currentPack.GetPackName();
            _levelNameText.text = gameWinInfo._currentPack.GetLevelInfos()[gameWinInfo._currentLevelNumber].Name;
        }

        private void SetNextLevel(GameWinInfo gameWinInfo)
        {
            _packImage.sprite = gameWinInfo._nextPack.GetPackSprite();
            _packNameText.text = gameWinInfo._nextPack.GetPackName();
            _levelNameText.text = gameWinInfo._nextPack.GetLevelInfos()[gameWinInfo._nextLevelNumber].Name;
        }

        private IEnumerator AnimateNextLevel(GameWinInfo gameWinInfo)
        {
            SetCurrentLevel(gameWinInfo);

            HidePackAndLevelInfo();
            yield return new WaitForSecondsRealtime(Mathf.Max(_scaleAnimationDuration, _rotationAnimationDuration));

            SetNextLevel(gameWinInfo);

            ShowPackAndLevelInfo();
            yield return new WaitForSecondsRealtime(Mathf.Max(_scaleAnimationDuration, _rotationAnimationDuration));
        }


        private IEnumerator AnimateChoosePack(GameWinInfo gameWinInfo)
        {
            SetCurrentLevel(gameWinInfo);

            HidePackAndLevelInfo();
            yield return new WaitForSecondsRealtime(Mathf.Max(_scaleAnimationDuration, _rotationAnimationDuration));

            _levelInfos.SetActive(false);

            if (IsLastLevel(gameWinInfo))
            {
                _allPacksPassed.SetActive(true);
                AnimationRotateAndScale(_allPacksPassed.GetComponent<RectTransform>(), new Vector3(0, 0, -360), 1f,
                    _rotationAnimationDuration, _scaleAnimationDuration);
            }
            else
            {
                _notEnoughEp.SetActive(true);
                AnimationRotateAndScale(_notEnoughEp.GetComponent<RectTransform>(), new Vector3(0, 0, -360), 1f,
                    _rotationAnimationDuration, _scaleAnimationDuration);
            }

            yield return new WaitForSecondsRealtime(Mathf.Max(_scaleAnimationDuration, _rotationAnimationDuration));
        }

        private void HidePackAndLevelInfo()
        {
            AnimationRotateAndScale(_packImage.GetComponent<RectTransform>(), new Vector3(0, 0, 360f), 0f,
                _rotationAnimationDuration, _scaleAnimationDuration);

            AnimationRotateAndScale(_packNameText.GetComponent<RectTransform>(), new Vector3(0, 0, 360f), 0f,
                _rotationAnimationDuration, _scaleAnimationDuration);

            AnimationRotateAndScale(_levelNameText.GetComponent<RectTransform>(), new Vector3(0, 0, 360f), 0f,
                _rotationAnimationDuration, _scaleAnimationDuration);
        }

        private void ShowPackAndLevelInfo()
        {
            AnimationRotateAndScale(_packImage.GetComponent<RectTransform>(), new Vector3(0, 0, -360f), 1f,
                _rotationAnimationDuration, _scaleAnimationDuration);

            AnimationRotateAndScale(_packNameText.GetComponent<RectTransform>(), new Vector3(0, 0, -360f), 1f,
                _rotationAnimationDuration, _scaleAnimationDuration);

            AnimationRotateAndScale(_levelNameText.GetComponent<RectTransform>(), new Vector3(0, 0, -360f), 1f,
                _rotationAnimationDuration, _scaleAnimationDuration);
        }

        private void AnimationRotateAndScale(RectTransform rect, Vector3 rotation, float scale,
            float rotationAnimationDuration, float scaleAnimationDuration)
        {
            rect.DORotate(rotation, rotationAnimationDuration, RotateMode.LocalAxisAdd);
            rect.DOScale(scale, scaleAnimationDuration);
        }

        private void SetButtonActivity(bool buttonNextLevel, bool buttonChoosePack)
        {
            _buttonNextLevel.gameObject.SetActive(buttonNextLevel);
            _buttonChoosePack.gameObject.SetActive(buttonChoosePack);
        }

        private IEnumerator CloseAnim()
        {
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