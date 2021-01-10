using System.IO;
using SaveLoader.Interfaces;
using UnityEngine;

namespace SaveLoader
{
    public class JsonSaveLoader : ILevelSaveLoader
    {
        private const string DefaultLevelPath = "Levels";

        public LevelInfo LoadLevel(string fileName)
        {
            var path = Path.Combine(DefaultLevelPath, fileName);
            var level = Resources.Load<TextAsset>(path);

            return JsonUtility.FromJson<LevelInfo>(level.text);
        }

        public LevelInfo LoadLevel(TextAsset level)
        {
            return JsonUtility.FromJson<LevelInfo>(level.text);
        }

        public void SaveLevel(LevelInfo info)
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