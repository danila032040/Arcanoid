namespace Scripts.Scenes.Start
{
    using Scripts.SceneLoader;
    using UnityEngine;
    using UnityEngine.UI;

    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartGame;

        [SerializeField] private SceneLoaderController _sceneLoader;


        private void Start()
        {
            _buttonStartGame.onClick.AddListener(() =>
            {
                _sceneLoader.LoadScene(LoadingScene.GameScene);
            });
        }
    }
}
