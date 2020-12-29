namespace Scripts.Pool
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class PoolContainer
    {
        [SerializeField] private GameObject _prefabToPool;
        [SerializeField] private Transform _container;

        [SerializeField] private int _amountToPool;
        [SerializeField] private bool _canExpand;

        private List<GameObject> _pooledObjects;


        public GameObject PrefabToPool => _prefabToPool;

        public void Init()
        {
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < _amountToPool; ++i)
            {
                AddNewObject();
            }
        }

        public GameObject Get()
        {
            foreach(GameObject obj in _pooledObjects)
            {
                if (!obj.gameObject.activeSelf && !obj.gameObject.activeInHierarchy)
                {
                    obj.SetActive(true);
                    obj.transform.SetParent(null);
                    return obj;
                }
            }

            if (_canExpand)
            {
                GameObject obj = AddNewObject();
                obj.SetActive(true);
                obj.transform.SetParent(null);
                return obj;
            }

            return null;
        }

        public void Remove(GameObject obj)
        {
            if (_pooledObjects.Contains(obj))
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(_container);
            }
        }

        private GameObject AddNewObject()
        {
            GameObject obj = UnityEngine.Object.Instantiate(_prefabToPool);
            _pooledObjects.Add(obj);
            Remove(obj);
            return obj;
        }
    }
}