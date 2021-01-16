using System;
using SaveLoader;
using SaveLoadSystem.Data;
using UnityEngine;

namespace Scenes.ChoosePack.Packs
{
    [RequireComponent(typeof(PackView))]
    public class Pack : MonoBehaviour
    {
        
        private PackInfo _packInfo;
        private PackView _packView;

        public void Init(PackInfo packInfo)
        {
            _packInfo = packInfo;
            
            
            _packView.SetPackName(_packInfo.GetPackName());
            _packView.SetPassedLevelsInfo($"0/{_packInfo.GetLevelsCount()}");
            _packView.SetPackSprite(_packInfo.GetPackSprite());
        }
        
        private void Awake()
        {
            _packView = GetComponent<PackView>();
        }

        public PackView GetPackView() => _packView;
        public PackInfo GetPackInfo() => _packInfo;
    }
}