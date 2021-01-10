using System;
using UnityEngine;

namespace SaveLoader
{
    [Serializable]
    public class PackData
    {
        [SerializeField] private string _packName;
        [SerializeField] private TextAsset[] _levelFiles;

        public string GetPackName() => _packName;
        public TextAsset[] GetLevelFiles() => _levelFiles;
        public int GetLevelsCount() => _levelFiles.Length;
    }
}