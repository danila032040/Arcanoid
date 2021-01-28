using Context;
using EnergySystem;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.SaveLoaders;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Start
{
    public class StartSceneController : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartGame;
        [SerializeField] private TextMeshProUGUI _energyPointsCountText;


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
            _energyPointsCountText.text = $"{EnergyManager.Instance.GetEnergyPointsCount()}/" +
                                          $"{ProjectContext.Instance.GetEnergyConfig().GetInitialEnergyPoints()}";
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
