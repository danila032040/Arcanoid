using Singleton;
using UnityEngine;

namespace Scenes.Context
{
    public class ProjectContext : MonoBehaviourSingletonPersistent<ProjectContext>
    {
        private const string ProjectPrefabConfigPlace = "Configurations/ProjectPrefabConfiguration";

        private ProjectPrefabsConfig _prefabsConfig;

        protected override void Init()
        {
            base.Init();
            _prefabsConfig = Resources.Load<ProjectPrefabsConfig>(ProjectPrefabConfigPlace);
        }

        public ProjectPrefabsConfig GetPrefabsConfig() => _prefabsConfig;
    }
}