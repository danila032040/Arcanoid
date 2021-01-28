using System;
using System.IO;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using UnityEngine;

namespace SaveLoadSystem
{
    //TODO: Divide logic into 2 different classes, serialize and deserialize arrays into json prefabs.
    public class InfoSaveLoader : ILevelInfoSaveLoader, IPlayerInfoSaveLoader
    {
        private const string OpenedPackKey = "openedPack";
        private const string LastPlayedLevelKey = "lastPlayed";
        
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
            if (!PlayerPrefs.HasKey(OpenedPackKey + "Count") ||
                !PlayerPrefs.HasKey(LastPlayedLevelKey + "Count")) return null;
            return new PlayerInfo(LoadOpenedPacksForPlayerInfo(), LoadLastPlayedLevelsForPlayerInfo());
        }

        

        private bool[] LoadOpenedPacksForPlayerInfo()
        {
            if (!PlayerPrefs.HasKey(OpenedPackKey + "Count")) return null;
            int n = PlayerPrefs.GetInt(OpenedPackKey + "Count");
            bool[] openedPacks = new bool[n];

            for (int i = 0; i < n; ++i)
            {
                openedPacks[i] = Convert.ToBoolean(PlayerPrefs.GetInt(OpenedPackKey + i));
            }

            return openedPacks;
        }
        private int[] LoadLastPlayedLevelsForPlayerInfo()
        {
            if (!PlayerPrefs.HasKey(LastPlayedLevelKey + "Count")) return null;
            int n = PlayerPrefs.GetInt(LastPlayedLevelKey + "Count");
            int[] data = new int[n];

            for (int i = 0; i < n; ++i)
            {
                data[i] = PlayerPrefs.GetInt(LastPlayedLevelKey + i);
            }

            return data;
        }

        public void SavePlayerInfo(PlayerInfo info)
        {
            //TODO: USE JSON UTILITY
            
            if (info == null)
            {
                PlayerPrefs.DeleteAll();
                return;
            }
            SaveOpenedPacksByPlayerInfo(info);
            SaveLastPlayedLevelsByPlayerInfo(info);
            PlayerPrefs.Save();
        }

        

        private void SaveOpenedPacksByPlayerInfo(PlayerInfo info)
        {
            bool[] openedPacks = info.GetOpenedPacks();
            int n = openedPacks.Length;
            PlayerPrefs.SetInt(OpenedPackKey + "Count", n);

            for (int i = 0; i < n; ++i)
            {
                PlayerPrefs.SetInt(OpenedPackKey + i, Convert.ToInt32(openedPacks[i]));
            }
        }
        private void SaveLastPlayedLevelsByPlayerInfo(PlayerInfo info)
        {
            int[] data = info.GetLastPlayedLevels();
            int n = data.Length;
            PlayerPrefs.SetInt(LastPlayedLevelKey + "Count", n);

            for (int i = 0; i < n; ++i)
            {
                PlayerPrefs.SetInt(LastPlayedLevelKey + i, data[i]);
            }
        }

        #endregion
    }
}