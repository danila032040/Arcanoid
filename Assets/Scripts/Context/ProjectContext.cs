using Configurations;
using Configurations.Constants;
using SaveLoadSystem;
using SaveLoadSystem.Interfaces;
using Singleton;
using UnityEngine;

namespace Context
{
    public class ProjectContext : MonoBehaviourSingletonPersistent<ProjectContext>, IMonoBehaviourSingletonInitialize<ProjectContext>
    {
        private const string ProjectContextConstantsPlace = "Configurations/Constants/ProjectContextConstants";
        
        private ProjectContextConstants _projectContextConstants;
        private NotifyPopUpLocalizationConstants _notifyPopUpLocalizationConstants;
        
        
        private ProjectPrefabsConfig _prefabsConfig;
        private HealthConfiguration _healthConfig;
        private EnergyConfiguration _energyConfig;
        
        private IPackProvider _packProvider;



        public void InitSingleton()
        {
            _projectContextConstants = Resources.Load<ProjectContextConstants>(ProjectContextConstantsPlace);
            _notifyPopUpLocalizationConstants =
                Resources.Load<NotifyPopUpLocalizationConstants>(_projectContextConstants
                    .NotifyPopUpLocalizationConstants);
            
            _prefabsConfig = Resources.Load<ProjectPrefabsConfig>(_projectContextConstants.ProjectPrefabsConfigPlace);
            _healthConfig = Resources.Load<HealthConfiguration>(_projectContextConstants.HealthConfigPlace);
            _energyConfig = Resources.Load<EnergyConfiguration>(_projectContextConstants.EnergyConfigPlace);
            
            _packProvider = Resources.Load<PackProvider>(_projectContextConstants.PackProviderPlace);
        }
        
        public ProjectContextConstants ProjectContextConstants => _projectContextConstants;
        public NotifyPopUpLocalizationConstants NotifyPopUpLocalizationConstants => _notifyPopUpLocalizationConstants;
        
        
        public ProjectPrefabsConfig GetPrefabsConfig() => _prefabsConfig;
        public HealthConfiguration GetHealthConfig() => _healthConfig;
        public EnergyConfiguration GetEnergyConfig() => _energyConfig;
        
        public IPackProvider GetPackProvider() => _packProvider;

    }
}