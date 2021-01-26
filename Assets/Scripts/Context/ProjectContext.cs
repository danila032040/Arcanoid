using SaveLoadSystem;
using Singleton;
using UnityEngine;

namespace Context
{
    public class ProjectContext : MonoBehaviourSingletonPersistent<ProjectContext>, IMonoBehaviourSingletonInitialize<ProjectContext>
    {
        private const string ProjectPrefabsConfigPlace = "Configurations/ProjectPrefabsConfiguration";

        private ProjectPrefabsConfig _prefabsConfig;

        public void InitSingleton()
        {
            _prefabsConfig = Resources.Load<ProjectPrefabsConfig>(ProjectPrefabsConfigPlace);
        }

        public void ClearSaves()
        {
            InfoSaveLoader saveLoader = new InfoSaveLoader();
            saveLoader.SavePlayerInfo(null);
        }
        

        public ProjectPrefabsConfig GetPrefabsConfig() => _prefabsConfig;
        
    }
}