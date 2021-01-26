using Scenes.Context;
using Singleton;
using UnityEngine;

namespace Scenes
{
    public class DataProviderBetweenScenes : MonoBehaviourSingletonPersistent<DataProviderBetweenScenes>, IMonoBehaviourSingletonInitialize<DataProviderBetweenScenes>
    {
        [SerializeField] private int _selectedPackNumber;
        
        public void InitSingleton()
        {
            Instance = ProjectContext.Instance.GetPrefabsConfig().GetPrefab<DataProviderBetweenScenes>();
        }
        
        public int GetSelectedPackNumber() => _selectedPackNumber;
        
        public void SetSelectedPackNumber(int packNumber)
        {
            _selectedPackNumber = packNumber;
        }

        
    }
}