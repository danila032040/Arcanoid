using Context;
using EnergySystem;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
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
        private IPackProvider _packProvider;

        [SerializeField] private PackProvider _packProviderImpl;

        public void Init(IPlayerInfoSaveLoader playerInfoSaveLoader, IPackProvider packProvider)
        {
            _playerInfoSaveLoader = playerInfoSaveLoader;
            _packProvider = packProvider;
        }

        private void Start()
        {
            Init(new PlayerInfoSaveLoader(), _packProviderImpl);
            _buttonStartGame.onClick.AddListener(StartGame);
        }

        
        private void StartGame()
        {
            _buttonStartGame.onClick.RemoveListener(StartGame);
            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo();

            if (info == null)
            {
                EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig().GetEnergyPointsToPlayLevel());
                _playerInfoSaveLoader.SavePlayerInfo(PlayerInfo.GetDefault(_packProvider.GetPackInfos().Length));
                SceneLoaderController.Instance.LoadScene(LoadingScene.GameScene);
            }
            else
            {
                SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
            }
        }
    }
}