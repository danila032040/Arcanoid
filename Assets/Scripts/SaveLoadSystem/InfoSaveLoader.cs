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
        private const string lastPlayedLevelKey = "lastPlayed";
        
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
            return new PlayerInfo(LoadOpenedPacksForPlayerInfo(), LoadLastPlayedLevelsForPlayerInfo());
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
        private int[] LoadLastPlayedLevelsForPlayerInfo()
        {
            if (!PlayerPrefs.HasKey(lastPlayedLevelKey + "Count")) return null;
            int n = PlayerPrefs.GetInt(lastPlayedLevelKey + "Count");
            int[] data = new int[n];

            for (int i = 0; i < n; ++i)
            {
                data[i] = PlayerPrefs.GetInt(lastPlayedLevelKey + i);
            }

            return data;
        }

        public void SavePlayerInfo(PlayerInfo info)
        {
            SaveOpenedPacksByPlayerInfo(info);
            SaveLastPlayedLevelsByPlayerInfo(info);
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
        private void SaveLastPlayedLevelsByPlayerInfo(PlayerInfo info)
        {
            int[] data = info.GetLastPlayedLevels();
            int n = data.Length;
            PlayerPrefs.SetInt(lastPlayedLevelKey + "Count", n);

            for (int i = 0; i < n; ++i)
            {
                PlayerPrefs.SetInt(lastPlayedLevelKey + i, data[i]);
            }
        }

        #endregion
    }
}