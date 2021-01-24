using System.Collections;
using DG.Tweening;
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
    //TODO: Use singleton.
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
