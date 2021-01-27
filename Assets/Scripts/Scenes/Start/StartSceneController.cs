using Context;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.SaveLoaders;
using SceneLoader;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Start
{
    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartGame;


        private IPlayerInfoSaveLoader _playerInfoSaveLoader;
        private IPackProvider _packProvider;

        [SerializeField] private PackProvider _packProviderImpl;
        public void Init(IPlayerInfoSaveLoader playerInfoSaveLoader, IPackProvider packProvider)
        {
            _playerInfoSaveLoader = playerInfoSaveLoader;
            _packProvider = packProvider;
        }

        private void Start()
        {
            Init(new InfoSaveLoader(), _packProviderImpl);
            _buttonStartGame.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo();
            
            _playerInfoSaveLoader.SavePlayerInfo(null);
            
            if (info == null)
            {
                _playerInfoSaveLoader.SavePlayerInfo(PlayerInfo.GetDefault(_packProvider.GetPackInfos().Length));
                SceneLoaderController.Instance.LoadScene(LoadingScene.GameScene);
            }
            else SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
        }

    }
}
