using System.Linq;
using System.Runtime.CompilerServices;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.SaveLoaders;
using SceneLoader;
using Scenes.ChoosePack.Packs;
using UnityEngine;

namespace Scenes.ChoosePack
{
    public class PackManager : MonoBehaviour
    {
        [SerializeField] private Pack _packPrefab;
        [SerializeField] private Transform _packsParent;

        [SerializeField] private PackProvider _test;
        
        private IPackProvider _packProvider;
        private IPlayerInfoSaveLoader _playerInfoSaveLoader;
        private DataProviderBetweenScenes _dataProvider;

        
        public void Init(IPackProvider packProvider, IPlayerInfoSaveLoader playerInfoSaveLoader, DataProviderBetweenScenes dataProvider)
        {
            _packProvider = packProvider;
            _playerInfoSaveLoader = playerInfoSaveLoader;
            _dataProvider = dataProvider;
        }
        
        private void Start()
        {
            Init(_test, new InfoSaveLoader(), DataProviderBetweenScenes.Instance);
            SpawnPacks();
        }

        private Pack[] _packs;
        public void SpawnPacks()
        {
            PackInfo[] packInfos = _packProvider.GetPackInfos();
            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo();
            int n = packInfos.Length;
            _packs = new Pack[n];

            if (info.GetOpenedPacks() == null)
            {
                info.SetOpenedPacks(new bool[n]);
                info.GetOpenedPacks()[0] = true;
                
            }

            if (info.GetLastPlayedLevels() == null)
            {
                info.SetLastPlayedLevels(new int[n]);
            }
            
            _playerInfoSaveLoader.SavePlayerInfo(info);
            
            for (int i = 0; i < n; ++i)
            {
                _packs[i] = Instantiate(_packPrefab, _packsParent);
                _packs[i].transform.SetAsFirstSibling();
                _packs[i].Init(packInfos[i], _playerInfoSaveLoader.LoadPlayerInfo(), _packProvider);

                _packs[i].Clicked += PackClicked;
                
                if (info.GetOpenedPacks()[i]) _packs[i].GetPackView().Show();
                else _packs[i].GetPackView().Hide();
            }
        }

        private void PackClicked(PackInfo packInfo)
        {
            int packNumber = _packProvider.GetPackNumber(packInfo);

            PlayerInfo playerInfo = _playerInfoSaveLoader.LoadPlayerInfo();
            int levelNumber = playerInfo.GetLastPlayedLevels()[packNumber];
            
            if (levelNumber == packInfo.GetLevelsCount()) levelNumber = 0;
            
            _dataProvider.SetCurrentLevelNumber(levelNumber);
            _dataProvider.SetCurrentPackNumber(packNumber);
            
            SceneLoaderController.Instance.LoadScene(LoadingScene.GameScene);
        }
    }
}