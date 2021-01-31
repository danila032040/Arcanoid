using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using UnityEngine;

namespace SaveLoadSystem
{
    public class EnergyPointsSaveLoader : IEnergyPointsSaveLoader
    {
        private const string EnergyPointsKey = "EnergyPoints";

        public void SaveEnergyPoints(EnergyInfo info)
        {
            if (info == null)
            {
                PlayerPrefs.DeleteKey(EnergyPointsKey);
                return;
            }

            PlayerPrefs.SetString(EnergyPointsKey, JsonUtility.ToJson(info, true));
            PlayerPrefs.Save();
        }

        public EnergyInfo LoadEnergyPoints()
        {
            if (!PlayerPrefs.HasKey(EnergyPointsKey)) return null;
            
            EnergyInfo ep = JsonUtility.FromJson<EnergyInfo>(PlayerPrefs.GetString(EnergyPointsKey));
            if (ep.LastTimeUpdated == 0) return null;
            return ep;
        }
    }
}