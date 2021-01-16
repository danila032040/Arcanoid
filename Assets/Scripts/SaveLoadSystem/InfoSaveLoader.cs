using System;
using System.IO;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.Infos;
using SaveLoadSystem.Interfaces.SaveLoaders;
using UnityEngine;

namespace SaveLoadSystem
{
    public class InfoSaveLoader : ILevelInfoSaveLoader, IPlayerInfoSaveLoader
    {
        private const string openedPackKey = "openedPack"; 
        
        #region ILevelInfoSaveLoader

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

        #endregion

        #region IPlayerInfoSaveLoader

        public PlayerInfo LoadPlayerInfo()
        {
            return new PlayerInfo(LoadOpenedPacksForPlayerInfo());
        }

        private bool[] LoadOpenedPacksForPlayerInfo()
        {
            if (!PlayerPrefs.HasKey(openedPackKey + "Count")) return null;
            int n = PlayerPrefs.GetInt(openedPackKey + "Count");
            bool[] openedPacks = new bool[n];

            for (int i = 0; i < n; ++i)
            {
                openedPacks[i] = Convert.ToBoolean(PlayerPrefs.GetInt(openedPackKey + i));
            }

            return openedPacks;
        }

        public void SavePlayerInfo(PlayerInfo info)
        {
            SaveOpenedPacksByPlayerInfo(info);
            PlayerPrefs.Save();
        }

        private void SaveOpenedPacksByPlayerInfo(PlayerInfo info)
        {
            bool[] openedPacks = info.GetOpenedPacks();
            int n = openedPacks.Length;
            PlayerPrefs.SetInt(openedPackKey + "Count", n);

            for (int i = 0; i < n; ++i)
            {
                PlayerPrefs.SetInt(openedPackKey + i, Convert.ToInt32(openedPacks[i]));
            }
        }

        #endregion
    }
}