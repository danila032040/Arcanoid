﻿namespace Scripts.Scenes.Start
{
    using Scripts.Localization;
    using Scripts.SceneLoader;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartGame;
        [SerializeField] private TextMeshProUGUI _buttonText;

        [SerializeField] private SceneLoaderController _sceneLoader;
        [SerializeField] private Localizer _localizer;

        private void Start()
        {
            LocalizeButtonText();
            _localizer.LocaleChanged += LocalizeButtonText;

            _buttonStartGame.onClick.AddListener(() =>
            {
                _sceneLoader.LoadScene(LoadingScene.GameScene);
            });

        }

        private void LocalizeButtonText()
        {
            _buttonText.text = _localizer.Localize("ButtonStartGame");
        }
    }
}
