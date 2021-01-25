using UnityEngine;

namespace Singleton
{
    public class MonoBehaviourSingletonPersistentPrefabManager : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        public GameObject GetPrefab() => _prefab;
    }
}