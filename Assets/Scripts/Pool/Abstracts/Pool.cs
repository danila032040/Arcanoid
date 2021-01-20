﻿using System;
using System.Collections.Generic;
using Pool.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Pool.Abstracts
{
    [Serializable]
    public abstract class Pool<T> : MonoBehaviour where T : IPoolable
    {
        [SerializeField] private int _amountToPool;
        [SerializeField] private bool _canExpand;

        private IPoolFactory<T> _factory;

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

            T def = default(T);
            OnPoolExit(def);
            return def;
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
