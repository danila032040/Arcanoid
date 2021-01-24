using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Start
{
    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartGame;

        [SerializeField] private SceneLoaderController _sceneLoader;

        private IPlayerInfoSaveLoader _playerInfoSaveLoader;

        public void Init(IPlayerInfoSaveLoader playerInfoSaveLoader)
        {
            _playerInfoSaveLoader = playerInfoSaveLoader;
        }

        private void Start()
        {
            Init(new InfoSaveLoader());
            _buttonStartGame.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo(); 
            if (info.GetOpenedPacks() == null) _sceneLoader.LoadScene(LoadingScene.GameScene);
            else _sceneLoader.LoadScene(LoadingScene.ChoosePackScene);
        }
    }
}
