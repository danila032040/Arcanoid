using System.Collections.Generic;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "ProjectPrefabsConfiguration", menuName = "Configurations/Project/Prefabs", order = 0)]
    public class ProjectPrefabsConfig : ScriptableObject
    {
        [SerializeField] private List<GameObject> _prefabs;

        public T GetPrefab<T>() where T : class
        {
            foreach (GameObject prefab in _prefabs)
            {
                T res = prefab.GetComponent<T>();
                if (res != null)
                {
                    return res;
                }
            }

            return null;
        }
    }
}