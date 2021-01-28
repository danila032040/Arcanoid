using UnityEngine;
using UnityEngine.Serialization;

namespace EnergySystem
{
    [System.Serializable]
    public class EnergyPoints
    {
        [SerializeField] private int _count;
        [SerializeField] private string _lastTimeUpdated;
        [SerializeField] private string _timePassed;

        public EnergyPoints() {}

        public EnergyPoints(int count,string lastTimeUpdated, string timePassed)
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

        public string LastTimeUpdated
        {
            get => _lastTimeUpdated;
            set => _lastTimeUpdated = value;
        }

        public string TimePassed
        {
            get => _timePassed;
            set => _timePassed = value;
        }
    }
}