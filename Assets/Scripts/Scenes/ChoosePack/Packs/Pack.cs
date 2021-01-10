using System;
using SaveLoader;
using UnityEngine;

namespace Scenes.ChoosePack.Packs
{
    [RequireComponent(typeof(PackView))]
    public class Pack : MonoBehaviour
    {
        
        [SerializeField] private PackData _packData;
        
        private PackView _packView;

        private void Awake()
        {
            _packView = GetComponent<PackView>();
        }

        private void Start()
        {
            _packView.SetPackName(_packData.GetPackName());
            _packView.SetPassedLevelsInfo($"0/{_packData.GetLevelsCount()}");
        }

        public PackView GetPackView() => _packView;
        public PackData GetPackData() => _packData;
    }
}