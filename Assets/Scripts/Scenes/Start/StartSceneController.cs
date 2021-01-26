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
            if (info.GetOpenedPacks() == null) SceneLoaderController.Instance.LoadScene(LoadingScene.GameScene);
            else SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
        }

    }
}
