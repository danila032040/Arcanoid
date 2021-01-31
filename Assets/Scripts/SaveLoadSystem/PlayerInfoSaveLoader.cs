using Context;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using UnityEngine;

namespace SaveLoadSystem
{
    public class PlayerInfoSaveLoader : IPlayerInfoSaveLoader
    {
        private const string PlayerInfoKey = "PlayerInfo";


        public PlayerInfo LoadPlayerInfo()
        {
            if (!PlayerPrefs.HasKey(PlayerInfoKey)) return null;
            PlayerInfo pi =  JsonUtility.FromJson<PlayerInfo>(PlayerPrefs.GetString(PlayerInfoKey));
            if (pi.GetOpenedPacks().Length != pi.GetLastPlayedLevels().Length ||
                (pi.GetOpenedPacks().Length == pi.GetLastPlayedLevels().Length &&
                 pi.GetOpenedPacks().Length != ProjectContext.Instance.GetPackProvider().GetPacksCount()))
                return null;
            return pi;
        }
        public void SavePlayerInfo(PlayerInfo info)
        {
            if (info == null)
            {
                PlayerPrefs.DeleteKey(PlayerInfoKey);
                return;
            }

            PlayerPrefs.SetString(PlayerInfoKey, JsonUtility.ToJson(info));
            PlayerPrefs.Save();
        }
    }
}