using System;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using UnityEngine;

namespace SaveLoadSystem
{
    [CreateAssetMenu(fileName = "PackProvider", menuName = "Packs/PackProvider", order = 0)]
    public class PackProvider : ScriptableObject, IPackProvider
    {
        [SerializeField] private PackInfo[] _packs;

        public PackInfo[] GetPackInfos() => _packs;
        
        public int GetPackNumber(PackInfo packInfo) => Array.IndexOf(_packs, packInfo);
        public PackInfo GetPackInfo(int packNumber) => _packs[packNumber];
        public int GetPacksCount() => _packs.Length;
    }
}