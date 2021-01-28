using System;
using System.Collections;
using Context;
using DG.Tweening;
using Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public enum LoadingScene
    {
        
        StartScene = 0,
        ChoosePackScene = 1,
        GameScene = 2,
    }

    public class SceneLoaderController : MonoBehaviourSingletonPersistent<SceneLoaderController>, IMonoBehaviourSingletonInitialize<SceneLoaderController>
    {
        [SerializeField] private CanvasGroup _sceneChangingCanvasGroup;

        [SerializeField] private float _fadeDuration;

        public void InitSingleton()
        {
            Instance = null;
            Instantiate(ProjectContext.Instance.GetPrefabsConfig().GetPrefab<SceneLoaderController>());
        }
        
        public void LoadScene(LoadingScene scene, Action<AsyncOperation> onCompleted = null)
        { 
            StartCoroutine(LoadSceneCoroutine(scene, onCompleted));
        }

        private IEnumerator LoadSceneCoroutine(LoadingScene scene, Action<AsyncOperation> onCompleted)
        {
            HideScene();
            yield return new WaitForSecondsRealtime(_fadeDuration);
            SceneManager.LoadSceneAsync((int) scene).completed += (asyncOperation)=>
            {
                onCompleted?.Invoke(asyncOperation);
                ShowScene();
            };
        }

        private void ShowScene()
        {
            _sceneChangingCanvasGroup.blocksRaycasts = true;
            _sceneChangingCanvasGroup.alpha = 1;
            _sceneChangingCanvasGroup.DOFade(0f, _fadeDuration);
            _sceneChangingCanvasGroup.blocksRaycasts = false;
        }

        private void HideScene()
        {
            _sceneChangingCanvasGroup.DOFade(1f, _fadeDuration);
            _sceneChangingCanvasGroup.blocksRaycasts = false;
        }

        
    }
}