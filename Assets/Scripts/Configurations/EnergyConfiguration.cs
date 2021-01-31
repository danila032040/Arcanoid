using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "EnergyConfiguration", menuName = "Configurations/Energy", order = 0)]
    public class EnergyConfiguration : ScriptableObject
    {
        [SerializeField] private int _secondsToRestoreOneEnergyPoint;
        [SerializeField] private int _energyPointsToPlayLevel;
        [SerializeField] private int _energyPointsForPassingLevel;
        [SerializeField] private int _initialEnergyPoints;
        [SerializeField] private int _energyPointsToByeOneHeart;
        [SerializeField] private int _minEnergyPointsCount;

        public int GetSecondsToRestoreOneEnergyPoint() => _secondsToRestoreOneEnergyPoint;
        public int GetEnergyPointsToPlayLevel() => _energyPointsToPlayLevel;
        public int GetEnergyPointsForPassingLevel() => _energyPointsForPassingLevel;
        public int GetInitialEnergyPoints() => _initialEnergyPoints;
        public int GetEnergyPointsToByeOneHeart() => _energyPointsToByeOneHeart;
        public int GetMinEnergyPointsCount() => _minEnergyPointsCount;
    }
}