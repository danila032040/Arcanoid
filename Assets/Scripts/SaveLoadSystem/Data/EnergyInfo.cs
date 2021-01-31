using EnergySystem;
using SaveLoadSystem.Interfaces.Infos;
using UnityEngine;

namespace SaveLoadSystem.Data
{
    [System.Serializable]
    public class EnergyInfo : IEnergyPointsInfo
    {
        [SerializeField] private int _count;
        [SerializeField] private long _lastTimeUpdated;
        [SerializeField] private long _timePassed;

        public EnergyInfo()
        {
        }

        public EnergyInfo(int count, long lastTimeUpdated, long timePassed)
        {
            _count = count;
            _lastTimeUpdated = lastTimeUpdated;
            _timePassed = timePassed;
        }

        public int Count
        {
            get => _count;
            set => _count = value;
        }

        public long LastTimeUpdated
        {
            get => _lastTimeUpdated;
            set => _lastTimeUpdated = value;
        }

        public long TimePassed
        {
            get => _timePassed;
            set => _timePassed = value;
        }
    }
}