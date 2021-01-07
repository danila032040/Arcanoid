﻿namespace Scripts.SaveLoader
{
    using Scripts.SaveLoader.Interfaces;
    using System.IO;
    using UnityEngine;
    public class JsonSaveLoader : ILevelSaveLoader
    {
        private readonly string _defaultLevelPath = "Levels";
        public LevelInfo LoadLevel(string name)
        {
            string path = Path.Combine(_defaultLevelPath, name);
            TextAsset level = Resources.Load<TextAsset>(path);

            return JsonUtility.FromJson<LevelInfo>(level.text);
        }

        public void SaveLevel(LevelInfo info)
        {
            string path = Path.Combine($"{Directory.GetCurrentDirectory()}\\Assets\\Resources\\", _defaultLevelPath, $"{info.Name}.json");
            Debug.Log(path);
            File.WriteAllText(path, JsonUtility.ToJson(info, true));
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}