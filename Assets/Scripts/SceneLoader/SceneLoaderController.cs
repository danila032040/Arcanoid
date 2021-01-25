using System.Collections;
using DG.Tweening;
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

    public class SceneLoaderController : MonoBehaviourSingletonPersistent<SceneLoaderController>
    {
        [SerializeField] private CanvasGroup _sceneChangingCanvasGroup;

        [SerializeField] private float _fadeDuration;

        public void LoadScene(LoadingScene scene)
        {
            HideScene();
            SceneManager.LoadSceneAsync((int) scene).completed += OnCompleted;
        }

        private IEnumerator LoadSceneCoroutine(LoadingScene scene)
        {
            yield return new WaitForSeconds(_fadeDuration);
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