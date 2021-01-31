using System;
using Configurations;
using Context;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using Singleton;
using Unity.Mathematics;

namespace EnergySystem
{
    public class EnergyManager : MonoBehaviourSingletonPersistent<EnergyManager>,
        IMonoBehaviourSingletonInitialize<EnergyManager>
    {
        private EnergyConfiguration _config;
        private IEnergyPointsSaveLoader _energyPointsSaveLoader;

        public void InitSingleton()
        {
            _config = ProjectContext.Instance.GetEnergyConfig();
            _energyPointsSaveLoader = new EnergyPointsSaveLoader();

            RecalculateEnergyPoints();
        }

        private EnergyInfo RecalculateEnergyPoints()
        {
            EnergyInfo energyInfo = _energyPointsSaveLoader.LoadEnergyPoints();

            if (energyInfo == null)
            {
                energyInfo = new EnergyInfo(_config.GetInitialEnergyPoints(), DateTime.Now.Ticks,
                    TimeSpan.Zero.Ticks);
            }
            else
            {
                DateTime prevDate = new DateTime(energyInfo.LastTimeUpdated);
                TimeSpan otherSeconds = new TimeSpan(energyInfo.TimePassed);
                DateTime currDate = DateTime.Now;

                int passedSeconds = (int) math.round((currDate - prevDate).TotalSeconds + otherSeconds.TotalSeconds);
                int restoredPoints = passedSeconds / _config.GetSecondsToRestoreOneEnergyPoint();
                TimeSpan remainSeconds =
                    TimeSpan.FromSeconds(passedSeconds % _config.GetSecondsToRestoreOneEnergyPoint());

                if (energyInfo.Count < _config.GetInitialEnergyPoints())
                {
                    energyInfo.Count =
                        math.min(energyInfo.Count + restoredPoints, _config.GetInitialEnergyPoints());
                }

                energyInfo.LastTimeUpdated = currDate.Ticks;
                energyInfo.TimePassed = remainSeconds.Ticks;
            }

            _energyPointsSaveLoader.SaveEnergyPoints(energyInfo);
            return energyInfo;
        }

        public int GetEnergyPointsCount()
        {
            return RecalculateEnergyPoints().Count;
        }

        public void AddEnergyPoints(int value)
        {
            EnergyInfo ep = RecalculateEnergyPoints();
            ep.Count += value;
            if (ep.Count < _config.GetMinEnergyPointsCount()) ep.Count = _config.GetMinEnergyPointsCount();
            _energyPointsSaveLoader.SaveEnergyPoints(ep);
        }

        public int GetRemainingSecondsToRestoreOnePoint()
        {
            EnergyInfo ep = RecalculateEnergyPoints();
            if (ep.Count >= _config.GetInitialEnergyPoints()) return 0;
            
            int res = _config.GetSecondsToRestoreOneEnergyPoint() - (int) (ep.TimePassed / TimeSpan.TicksPerSecond);
            if (res == 0) _config.GetSecondsToRestoreOneEnergyPoint();
            
            return res;
        }

        public bool CanPlayLevel()
        {
            return GetEnergyPointsCount() + _config.GetEnergyPointsToPlayLevel() >= _config.GetMinEnergyPointsCount();
        }
    }
}