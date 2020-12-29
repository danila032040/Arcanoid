namespace Scripts.Pool.Abstracts
{
    using Scripts.Pool.Interfaces;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public abstract class Pool<T> : MonoBehaviour where T : IPoolable
    {
        [SerializeField] private int _amountToPool;
        [SerializeField] private bool _canExpand;

        [SerializeField] private IPoolFactory<T> _factory;

        private readonly Queue<T> _pooledObjects = new Queue<T>();

        public void Init(IPoolFactory<T> factory)
        {
            _factory = factory;

            for (int i = 0; i < _amountToPool; ++i)
            {
                T obj = _factory.Create();
                _pooledObjects.Enqueue(obj);
                OnPoolEnter(obj);
            }
        }
        public T Get()
        {
            if (_pooledObjects.Count > 0)
            {
                T obj = _pooledObjects.Dequeue();
                OnPoolExit(obj);
                return obj;
            }

            if (_canExpand)
            {
                T obj = _factory.Create();
                OnPoolExit(obj);
                return obj;
            }

            OnPoolExit(default(T));
            return default(T);
        }
        public void Remove(T obj)
        {
            _pooledObjects.Enqueue(obj);
            OnPoolEnter(obj);
        }

        protected abstract void OnPoolEnter(T obj);
        protected abstract void OnPoolExit(T obj);
    }
}
