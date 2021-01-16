using SaveLoadSystem.Data;
using UnityEngine;

namespace SaveLoadSystem.Interfaces.SaveLoaders
{
    public interface ILevelInfoSaveLoader
    {
        LevelInfo LoadLevelInfo(string fileName);
        LevelInfo LoadLevelInfo(TextAsset level);
        
        void SaveLevelInfo(LevelInfo info);
    }
}