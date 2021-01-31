using Configurations;
using SaveLoadSystem;
using SaveLoadSystem.Interfaces;
using Singleton;
using UnityEngine;

namespace Context
{
    public class ProjectContext : MonoBehaviourSingletonPersistent<ProjectContext>, IMonoBehaviourSingletonInitialize<ProjectContext>
    {
        private const string ProjectPrefabsConfigPlace = "Configurations/ProjectPrefabsConfiguration";
        private const string HealthConfigPlace = "Configurations/HealthConfiguration";
        private const string EnergyConfigPlace = "Configurations/EnergyConfiguration";
        
        private const string PackProviderPlace = "Packs/PackProvider";

        private ProjectPrefabsConfig _prefabsConfig;
        private HealthConfiguration _healthConfig;
        private EnergyConfiguration _energyConfig;
        
        private IPackProvider _packProvider;

        public void InitSingleton()
        {
            _prefabsConfig = Resources.Load<ProjectPrefabsConfig>(ProjectPrefabsConfigPlace);
            _healthConfig = Resources.Load<HealthConfiguration>(HealthConfigPlace);
            _energyConfig = Resources.Load<EnergyConfiguration>(EnergyConfigPlace);
            
            _packProvider = Resources.Load<PackProvider>(PackProviderPlace);
        }
        
        public ProjectPrefabsConfig GetPrefabsConfig() => _prefabsConfig;

        public HealthConfiguration GetHealthConfig() => _healthConfig;
        public EnergyConfiguration GetEnergyConfig() => _energyConfig;
        
        public IPackProvider GetPackProvider() => _packProvider;

    }
}