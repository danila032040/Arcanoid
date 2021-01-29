using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "HealthConfiguration", menuName = "Configurations/Health", order = 0)]
    public class HealthConfiguration : ScriptableObject
    {
        [SerializeField] private int _addHealthToBlockForCollisionWithBall;
        [SerializeField] private int _addHealthToPlayerForLoosingAllBalls;
        
        [SerializeField] private int _minBlockHealthValue;
        [SerializeField] private int _initialPlayerHealthValue;
        [SerializeField] private int _minPlayerHealthValue;


        public int AddHealthToBlockForCollisionWithBall => _addHealthToBlockForCollisionWithBall;
        public int AddHealthToPlayerForLoosingAllBalls => _addHealthToPlayerForLoosingAllBalls;
        public int MinBlockHealthValue => _minBlockHealthValue;

        
        public int InitialPlayerHealthValue => _initialPlayerHealthValue;
        public int MinPlayerHealthValue => _minPlayerHealthValue;
    }
}