﻿namespace Scripts.SceneLoader
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using DG.Tweening;

    public enum LoadingScene
    {
        GameScene = 0,
        StartScene = 1
    }

    public class SceneLoaderController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _sceneChangingCanvasGroup;

        [SerializeField] private float _fadeDuration;

        private void Awake()
        {
            _sceneChangingCanvasGroup.blocksRaycasts = true;
            _sceneChangingCanvasGroup.alpha = 1;
            _sceneChangingCanvasGroup.DOFade(0f, _fadeDuration);
            _sceneChangingCanvasGroup.blocksRaycasts = false;
        }

        public void LoadScene(LoadingScene scene)
        {
            StartCoroutine(LoadSceneCoroutine(scene));
        }

        private IEnumerator LoadSceneCoroutine(LoadingScene scene)
        {
            _sceneChangingCanvasGroup.DOFade(1f, _fadeDuration);
            _sceneChangingCanvasGroup.blocksRaycasts = false;
            yield return new WaitForSeconds(_fadeDuration);
            SceneManager.LoadScene((int)scene);
        }
    }
}
