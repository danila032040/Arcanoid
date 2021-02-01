using System.Collections;
using Context;
using DG.Tweening;
using EnergySystem;
using Localization.LocalizedText;
using PopUpSystems;
using SceneLoader;
using Scenes.Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Shared.PopUps
{
    public class RestartGamePopUp : PopUp
    {

        [SerializeField] private GameObject _panelRestart;
        [SerializeField] private GameObject _panelChoosePack;

        [SerializeField] private CanvasGroup _canvasGroup;
        
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _pauseAnimationDuration;

        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonByeOneHeart;
        [SerializeField] private Button _buttonChoosePack;

        [SerializeField] private LocalizedTextTmpUGui _restartMessageText;

        private GameManager _gameManager;
        
        public override void EnableInput()
        {
            _canvasGroup.interactable = true;
        }

        public override void DisableInput()
        {
            _canvasGroup.interactable = false;
        }
        
        public void Show(GameManager gameManager, bool stopTime)
        {
            _gameManager = gameManager;
            SetMessageText(EnergyManager.Instance.CanPlayLevel()
                ? ProjectContext.Instance.RestartGamePopUpLocalizationConstants.GameOver
                : ProjectContext.Instance.RestartGamePopUpLocalizationConstants.NotEnoughEnergy);
            
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonChoosePack.onClick.AddListener(OnButtonChoosePackClicked);
            _buttonByeOneHeart.onClick.AddListener(OnButtonBuyOneHeartClicked);
            
            StartCoroutine(ShowCoroutine(stopTime));
        }

        private void SetMessageText(string messageTextKey)
        {
            _restartMessageText.SetKey(messageTextKey);
            
            if (messageTextKey == ProjectContext.Instance.RestartGamePopUpLocalizationConstants.GameOver)
            {
                _panelRestart.SetActive(true);
                _panelChoosePack.SetActive(false);
                
                _buttonByeOneHeart.gameObject.SetActive(EnergyManager.Instance.CanBuyHeart());
            }
            else
            {
                _panelRestart.SetActive(false);
                _panelChoosePack.SetActive(true);
                _buttonByeOneHeart.gameObject.SetActive(false);
            }
        }

        private void Hide(bool returnTime)
        {
            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
            _buttonChoosePack.onClick.RemoveListener(OnButtonChoosePackClicked);
            _buttonByeOneHeart.onClick.RemoveListener(OnButtonBuyOneHeartClicked);
            
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
        
        private void OnButtonBuyOneHeartClicked()
        {
            StartCoroutine(BuyHeartCoroutine());
        }

        

        private void OnButtonChoosePackClicked()
        {
            SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
            Hide(false);
        }

        private void OnButtonRestartClicked()
        {
            StartCoroutine(GameRestartCoroutine());
        }

        private IEnumerator GameRestartCoroutine()
        {
            yield return _gameManager.GameRestartCoroutine();
            Hide(true);
        }
        
        private IEnumerator BuyHeartCoroutine()
        {
            yield return _gameManager.BuyHeartCoroutine();
            Hide(true);
        }
    }
}