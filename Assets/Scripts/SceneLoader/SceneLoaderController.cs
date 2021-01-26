using System.Collections;
using DG.Tweening;
using Scenes.Context;
using Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public enum LoadingScene
    {
        GameScene = 0,
        StartScene = 1,
        ChoosePackScene = 2
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
        
        public void LoadScene(LoadingScene scene)
        { 
            StartCoroutine(LoadSceneCoroutine(scene));
        }

        private IEnumerator LoadSceneCoroutine(LoadingScene scene)
        {
            HideScene();
            yield return new WaitForSeconds(_fadeDuration);
            SceneManager.LoadSceneAsync((int) scene).completed += OnCompleted;
        }

        private void OnCompleted(AsyncOperation obj)
        {
            ShowScene();
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