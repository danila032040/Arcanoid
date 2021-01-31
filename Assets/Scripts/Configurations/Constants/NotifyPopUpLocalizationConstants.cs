using UnityEngine;

namespace Configurations.Constants
{
    [CreateAssetMenu(fileName = "NotifyPopUpLocalizationConstants", menuName = "Configurations/Constants/Localization/NotifyPopUp", order = 0)]
    public class NotifyPopUpLocalizationConstants : ScriptableObject
    {
        [SerializeField] private string _ok = "Ok";
        [SerializeField] private string _choosePack = "ChoosePack";
        [SerializeField] private string _notEnoughEnergy = "NotEnoughEnergy";


        public string Ok => _ok;
        public string ChoosePack => _choosePack;
        public string NotEnoughEnergy => _notEnoughEnergy;
    }
}