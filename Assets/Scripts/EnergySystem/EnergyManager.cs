using System;
using System.Globalization;
using Context;
using Singleton;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EnergySystem
{
    public class EnergyManager : MonoBehaviourSingletonPersistent<EnergyManager>, IMonoBehaviourSingletonInitialize<EnergyManager>
    {
        private const string EnergyPointsKey = "EnergyPoints";
        private EnergyConfiguration _config;
        
        public void InitSingleton()
        {
            _config = ProjectContext.Instance.GetEnergyConfig();

            RecalculateEnergyPoints();
        }

        private void RecalculateEnergyPoints()
        {
            EnergyPoints energyPoints = LoadEnergyPoints();

            if (energyPoints == null)
            {
                energyPoints = new EnergyPoints(_config.GetInitialEnergyPoints(), DateTime.Now.ToString());
            }
            else
            {
                DateTime prevDate = DateTime.Parse(energyPoints.LastTimeUpdated);
                DateTime currDate = DateTime.Now;

                int passedSeconds = (prevDate - currDate).Seconds;
                int restoredPoints = passedSeconds / _config.GetSecondsToRestoreOneEnergyPoint();

                if (energyPoints.Count < _config.GetInitialEnergyPoints())
                {
                    energyPoints.Count =
                        math.max(energyPoints.Count + restoredPoints, _config.GetInitialEnergyPoints());
                }
                
                energyPoints.LastTimeUpdated = currDate.ToString();
            }
            
            SaveEnergyPoints(energyPoints);
        }

        public int GetEnergyPointsCount()
        {
            RecalculateEnergyPoints();
            return LoadEnergyPoints().Count;
        }

        public void AddEnergyPoints(int value)
        {
            RecalculateEnergyPoints();
            
            EnergyPoints ep = LoadEnergyPoints();
            ep.Count += value;
            SaveEnergyPoints(ep);
        }
        
        private void SaveEnergyPoints(EnergyPoints points)
        {
            PlayerPrefs.SetString(EnergyPointsKey, JsonUtility.ToJson(points, true));
            PlayerPrefs.Save();
        }

        private EnergyPoints LoadEnergyPoints()
        {
            if (!PlayerPrefs.HasKey(EnergyPointsKey)) return null;

            return JsonUtility.FromJson<EnergyPoints>(PlayerPrefs.GetString(EnergyPointsKey));
        }
    }
}