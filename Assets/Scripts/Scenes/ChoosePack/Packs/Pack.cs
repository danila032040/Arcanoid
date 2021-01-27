using System;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.Infos;
using UnityEngine;

namespace Scenes.ChoosePack.Packs
{
    [RequireComponent(typeof(PackView))]
    public class Pack : MonoBehaviour
    {
        public event Action<PackInfo> Clicked;
        
        private PackInfo _packInfo;
        private PackView _packView;

        public void Init(PackInfo packInfo, IPlayerPackInfo playerInfo, IPackProvider packProvider)
        {
            _packInfo = packInfo;
            
            
            _packView.SetPackName(_packInfo.GetPackName());
            int lastPlayedLevel = playerInfo.GetLastPlayedLevels()[packProvider.GetPackNumber(_packInfo)];
            _packView.SetPassedLevelsInfo($"{lastPlayedLevel}/{_packInfo.GetLevelsCount()}");
            _packView.SetPackSprite(_packInfo.GetPackSprite());
        }
        
        private void Awake()
        {
            _packView = GetComponent<PackView>();
            _packView.Clicked += () => OnClicked(_packInfo);
        }

        public PackView GetPackView() => _packView;
        public PackInfo GetPackInfo() => _packInfo;

        private void OnClicked(PackInfo obj)
        {
            Clicked?.Invoke(obj);
        }
    }
}