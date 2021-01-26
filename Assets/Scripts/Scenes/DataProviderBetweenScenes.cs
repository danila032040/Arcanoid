using Scenes.Context;
using Singleton;
using UnityEngine;

namespace Scenes
{
    public class DataProviderBetweenScenes : MonoBehaviourSingletonPersistent<DataProviderBetweenScenes>,
        IMonoBehaviourSingletonInitialize<DataProviderBetweenScenes>
    {
        [SerializeField] private int _currentPackNumber;
        [SerializeField] private int _currentLevelNumber;

        public void InitSingleton()
        {
            Instance = null;
            Instantiate(ProjectContext.Instance.GetPrefabsConfig().GetPrefab<DataProviderBetweenScenes>());
        }

        public int GetCurrentPackNumber() => _currentPackNumber;
        public int GetCurrentLevelNumber() => _currentLevelNumber;

        public void SetCurrentPackNumber(int packNumber) => _currentPackNumber = packNumber;
        public void SetCurrentLevelNumber(int levelNumber) => _currentLevelNumber = levelNumber;
    }
}