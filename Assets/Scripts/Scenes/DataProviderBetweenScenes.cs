using SaveLoadSystem.Data;
using UnityEngine;

namespace Scenes
{
    public class DataProviderBetweenScenes : MonoBehaviourSingletonPersistent<DataProviderBetweenScenes>
    {
        [SerializeField] private int _selectedPackNumber;
        
        public int GetSelectedPackNumber() => _selectedPackNumber;
        
        public void SetSelectedPackNumber(int packNumber)
        {
            _selectedPackNumber = packNumber;
        }
        
    }
}