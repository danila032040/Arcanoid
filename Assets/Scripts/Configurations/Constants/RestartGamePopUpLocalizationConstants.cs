using UnityEngine;

namespace Configurations.Constants
{
    [CreateAssetMenu(fileName = "RestartGamePopUpLocalizationConstants", menuName = "Configurations/Constants/Localization/RestartGamePopUp", order = 0)]
    public class RestartGamePopUpLocalizationConstants : ScriptableObject
    {
        [SerializeField] private string _gameOver = "GameOver";
        [SerializeField] private string _notEnoughEnergy = "NotEnoughEnergy";

        public string GameOver => _gameOver;
        public string NotEnoughEnergy => _notEnoughEnergy;
    }
}