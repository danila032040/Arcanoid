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

        public PackInfo()
        {
            Init(new LevelInfoSaveLoader());
        }

        public void Init(ILevelInfoSaveLoader levelInfoSaveLoader)
        {
            _levelInfoSaveLoader = levelInfoSaveLoader;
        }

        public string GetPackName() => _packName;

        public LevelInfo[] GetLevelInfos()
        {
            int n = _levelFiles.Length;
            LevelInfo[] levelInfos = new LevelInfo[n];
            for (int i = 0; i < n; ++i)
            {
                levelInfos[i] = _levelInfoSaveLoader.LoadLevelInfo(_levelFiles[i]);
            }

            return levelInfos;
        }

        public int GetLevelsCount() => _levelFiles.Length;

        public Sprite GetPackSprite() => _packSprite;
    }
}