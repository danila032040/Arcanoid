namespace Scripts.Pool
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private List<PoolContainer> _poolContainers = new List<PoolContainer>();

        private readonly Dictionary<GameObject, PoolContainer> _poolPrefabsDictionary = new Dictionary<GameObject, PoolContainer>();

        private void Start()
        {
            foreach(PoolContainer container in _poolContainers)
            {
                container.Init();
                _poolPrefabsDictionary[container.PrefabToPool] = container;
            }
        }


        public GameObject Get(GameObject orig)
        {
            return _poolPrefabsDictionary[orig].Get();
        }

        public void Remove(GameObject orig, GameObject obj)
        {
            _poolPrefabsDictionary[orig].Remove(obj);
        }
    }
}
