using System;
using Context;
using Singleton;
using Unity.Mathematics;
using UnityEngine;

namespace EnergySystem
{
    public class EnergyManager : MonoBehaviourSingletonPersistent<EnergyManager>,
        IMonoBehaviourSingletonInitialize<EnergyManager>
    {
        private const string EnergyPointsKey = "EnergyPoints";
        private EnergyConfiguration _config;

        public void InitSingleton()
        {
            _config = ProjectContext.Instance.GetEnergyConfig();

            RecalculateEnergyPoints();
        }

        private EnergyPoints RecalculateEnergyPoints()
        {
            EnergyPoints energyPoints = LoadEnergyPoints();

            if (energyPoints == null)
            {
                energyPoints = new EnergyPoints(_config.GetInitialEnergyPoints(), DateTime.Now.Ticks,
                    TimeSpan.Zero.Ticks);
            }
            else
            {
                DateTime prevDate = new DateTime(energyPoints.LastTimeUpdated);
                TimeSpan otherSeconds = new TimeSpan(energyPoints.TimePassed);
                DateTime currDate = DateTime.Now;

                int passedSeconds = (int) math.round((currDate - prevDate).TotalSeconds + otherSeconds.TotalSeconds);
                int restoredPoints = passedSeconds / _config.GetSecondsToRestoreOneEnergyPoint();
                TimeSpan remainSeconds =
                    TimeSpan.FromSeconds(passedSeconds % _config.GetSecondsToRestoreOneEnergyPoint());

                if (energyPoints.Count < _config.GetInitialEnergyPoints())
                {
                    energyPoints.Count =
                        math.min(energyPoints.Count + restoredPoints, _config.GetInitialEnergyPoints());
                }

                energyPoints.LastTimeUpdated = currDate.Ticks;
                energyPoints.TimePassed = remainSeconds.Ticks;
            }

            SaveEnergyPoints(energyPoints);
            return energyPoints;
        }

        public int GetEnergyPointsCount()
        {
            return RecalculateEnergyPoints().Count;
        }

        public void AddEnergyPoints(int value)
        {
            EnergyPoints ep = RecalculateEnergyPoints();
            ep.Count += value;
            if (ep.Count < _config.GetMinEnergyPointsCount()) ep.Count = _config.GetMinEnergyPointsCount();
            ;
            SaveEnergyPoints(ep);
        }

        public int GetRemainingSecondsToRestoreOnePoint()
        {
            EnergyPoints ep = RecalculateEnergyPoints();
            if (ep.Count == _config.GetInitialEnergyPoints()) return 0;
            
            int res = _config.GetSecondsToRestoreOneEnergyPoint() - (int) (ep.TimePassed / TimeSpan.TicksPerSecond);
            if (res == 0) _config.GetSecondsToRestoreOneEnergyPoint();
            
            return res;
        }

        public bool CanPlayLevel()
        {
            return GetEnergyPointsCount() + _config.GetEnergyPointsToPlayLevel() >= _config.GetMinEnergyPointsCount();
        }

        private void SaveEnergyPoints(EnergyPoints points)
        {
            if (points == null)
            {
                PlayerPrefs.DeleteKey(EnergyPointsKey);
                return;
            }

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