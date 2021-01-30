using UnityEngine;
using UnityEngine.Serialization;

namespace EnergySystem
{
    [System.Serializable]
    public class EnergyPoints
    {
        [SerializeField] private int _count;
        [SerializeField] private long _lastTimeUpdated;
        [SerializeField] private long _timePassed;

        public EnergyPoints()
        {
        }

        public EnergyPoints(int count, long lastTimeUpdated, long timePassed)
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