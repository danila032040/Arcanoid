using Singleton;
using UnityEngine;

namespace Scenes.Context
{
    public class ProjectContext : MonoBehaviourSingletonPersistent<ProjectContext>
    {
        private const string ProjectPrefabsConfigPlace = "Configurations/ProjectPrefabsConfiguration";

        private ProjectPrefabsConfig _prefabsConfig;

        protected override void Init()
        {
            base.Init();
            _prefabsConfig = Resources.Load<ProjectPrefabsConfig>(ProjectPrefabsConfigPlace);
        }

        public ProjectPrefabsConfig GetPrefabsConfig() => _prefabsConfig;
    }
}