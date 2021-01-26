using Singleton;
using UnityEngine;

namespace Scenes.Context
{
    public class ProjectContext : MonoBehaviourSingletonPersistent<ProjectContext>, IMonoBehaviourSingletonInitialize<ProjectContext>
    {
        private const string ProjectPrefabsConfigPlace = "Configurations/ProjectPrefabsConfiguration";

        private ProjectPrefabsConfig _prefabsConfig;

        public void InitSingleton()
        {
            _prefabsConfig = Resources.Load<ProjectPrefabsConfig>(ProjectPrefabsConfigPlace);
        }

        public ProjectPrefabsConfig GetPrefabsConfig() => _prefabsConfig;
        
    }
}