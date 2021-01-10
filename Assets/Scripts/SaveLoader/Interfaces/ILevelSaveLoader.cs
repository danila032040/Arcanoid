using UnityEngine;

namespace SaveLoader.Interfaces
{
    public interface ILevelSaveLoader
    {
        LevelInfo LoadLevel(string fileName);
        LevelInfo LoadLevel(TextAsset level);
        
        void SaveLevel(LevelInfo info);
    }
}