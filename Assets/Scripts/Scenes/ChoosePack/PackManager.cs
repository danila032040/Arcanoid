using System.Linq;
using System.Runtime.CompilerServices;
using SaveLoader;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
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

        
        public void Init(IPackProvider packProvider, IPlayerInfoSaveLoader playerInfoSaveLoader)
        {
            _packProvider = packProvider;
            _playerInfoSaveLoader = playerInfoSaveLoader;
        }
        
        private void Start()
        {
            Init(_test, new InfoSaveLoader());
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
            Debug.Log(packInfo.GetPackName());
        }
    }
}