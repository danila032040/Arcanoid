using Localization;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Start
{
    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartGame;

        [SerializeField] private SceneLoaderController _sceneLoader;

        private void Start()
        {
            _buttonStartGame.onClick.AddListener(() => { _sceneLoader.LoadScene(LoadingScene.GameScene); });

        }
    }
}
