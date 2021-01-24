using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "HealthConfiguration", menuName = "Configurations/Health", order = 0)]
    public class HealthConfiguration : ScriptableObject
    {
        [SerializeField] private int _addHealthForCollisionWithBall;


        public int AddHealthForCollisionWithBall => _addHealthForCollisionWithBall;
    }
}