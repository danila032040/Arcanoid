using System.Linq;
using System.Runtime.CompilerServices;
using SaveLoader;
using SaveLoadSystem;
using SaveLoadSystem.Data;
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
        [SerializeField] private SceneLoaderController _sceneLoader;
        
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
                info.GetOpenedPacks()[n - 1] = true;
                
                _playerInfoSaveLoader.SavePlayerInfo(info);
            }
            
            for (int i = 0; i < n; ++i)
            {
                _packs[i] = Instantiate(_packPrefab, _packsParent);
                _packs[i].Init(packInfos[i]);

                _packs[i].OnClicked += PackClicked;
                
                if (info.GetOpenedPacks()[i]) _packs[i].GetPackView().Show();
                else _packs[i].GetPackView().Hide();
            }
        }

        private void PackClicked(PackInfo packInfo)
        {
            _dataProvider.SetSelectedPackInfo(packInfo);
            _sceneLoader.LoadScene(LoadingScene.GameScene);
        }
    }
}