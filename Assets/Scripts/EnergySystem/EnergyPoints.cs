using UnityEngine;
using UnityEngine.Serialization;

namespace EnergySystem
{
    [System.Serializable]
    public class EnergyPoints
    {
        [SerializeField] private int _count;
        [SerializeField] private string _lastTimeUpdated;

        public EnergyPoints() {}

        public EnergyPoints(int count, string lastTimeUpdated)
        {
            _count = count;
            _lastTimeUpdated = lastTimeUpdated;
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
    }
}