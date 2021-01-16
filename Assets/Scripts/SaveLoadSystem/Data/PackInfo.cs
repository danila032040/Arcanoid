using System;
using SaveLoadSystem.Interfaces.SaveLoaders;
using UnityEngine;

namespace SaveLoadSystem.Data
{
    [Serializable]
    public class PackInfo
    {
        [SerializeField] private string _packName;
        [SerializeField] private TextAsset[] _levelFiles;
        [SerializeField] private Sprite _packSprite;


        private ILevelInfoSaveLoader _levelInfoSaveLoader;
        private LevelInfo[] _levelInfos;

        public PackInfo()
        {
            Init(new InfoSaveLoader());
        }
        
        public void Init(ILevelInfoSaveLoader levelInfoSaveLoader)
        {
            _levelInfoSaveLoader = levelInfoSaveLoader;
        }
        
        public string GetPackName() => _packName;

        public LevelInfo[] GetLevelInfos()
        {
            if (_levelInfos == null)
            {
                int n = _levelFiles.Length;
                _levelInfos = new LevelInfo[n];
                for (int i = 0; i < n; ++i)
                {
                    _levelInfos[i] = _levelInfoSaveLoader.LoadLevelInfo(_levelFiles[i]);
                }
            }
            return _levelInfos;
        }

        public int GetLevelsCount() => _levelFiles.Length;

        public Sprite GetPackSprite() => _packSprite;
    }
}