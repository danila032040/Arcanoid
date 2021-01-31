using System.IO;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using UnityEngine;

namespace SaveLoadSystem
{
    public class LevelInfoSaveLoader : ILevelInfoSaveLoader
    {
        private const string DefaultLevelPath = "Levels";

        public LevelInfo LoadLevelInfo(string fileName)
        {
            var path = Path.Combine(DefaultLevelPath, fileName);
            var level = Resources.Load<TextAsset>(path);

            return JsonUtility.FromJson<LevelInfo>(level.text);
        }

        public LevelInfo LoadLevelInfo(TextAsset level)
        {
            return JsonUtility.FromJson<LevelInfo>(level.text);
        }

        public void SaveLevelInfo(LevelInfo info)
        {
            var path = Path.Combine($"{Directory.GetCurrentDirectory()}\\Assets\\Resources\\", DefaultLevelPath,
                $"{info.FileName}.json");
            Debug.Log(path);
            File.WriteAllText(path, JsonUtility.ToJson(info, true));

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}