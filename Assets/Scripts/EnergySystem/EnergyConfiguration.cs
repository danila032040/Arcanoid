using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace EnergySystem
{
    [CreateAssetMenu(fileName = "EnergyConfiguration", menuName = "Configurations/Energy", order = 0)]
    public class EnergyConfiguration : ScriptableObject
    {
        [SerializeField] private int _secondsToRestoreOneEnergyPoint;
        [SerializeField] private int _energyPointsToPlayLevel;
        [SerializeField] private int _energyPointsForPassingLevel;
        [SerializeField] private int _initialEnergyPoints;
        [SerializeField] private int _energyPointsToByeOneHeart;

        public int GetSecondsToRestoreOneEnergyPoint() => _secondsToRestoreOneEnergyPoint;
        public int GetEnergyPointsToPlayLevel() => _energyPointsToPlayLevel;
        public int GetEnergyPointsForPassingLevel() => _energyPointsForPassingLevel;
        public int GetInitialEnergyPoints() => _initialEnergyPoints;
        public int GetEnergyPointsToByeOneHeart() => _energyPointsToByeOneHeart;
    }
}