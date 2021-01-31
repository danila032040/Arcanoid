using UnityEngine;

namespace Configurations.Constants
{
    [CreateAssetMenu(fileName = "ProjectContextConstants", menuName = "Configurations/Constants/ProjectContext", order = 0)]
    public class ProjectContextConstants : ScriptableObject
    {
        [SerializeField] private string _notifyPopUpLocalizationConstants = "Configurations/Constants/NotifyPopUpLocalizationConstants";
        
        [SerializeField] private string _projectPrefabsConfigPlace = "Configurations/ProjectPrefabsConfiguration";
        [SerializeField] private string _healthConfigPlace = "Configurations/HealthConfiguration";
        [SerializeField] private string _energyConfigPlace = "Configurations/EnergyConfiguration";
        
        [SerializeField] private string _packProviderPlace = "Packs/PackProvider";
        

        
        public string NotifyPopUpLocalizationConstants => _notifyPopUpLocalizationConstants;
        
        public string ProjectPrefabsConfigPlace => _projectPrefabsConfigPlace;
        public string HealthConfigPlace => _healthConfigPlace;
        public string EnergyConfigPlace => _energyConfigPlace;
        public string PackProviderPlace => _packProviderPlace;

    }
}