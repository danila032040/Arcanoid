using System;
using System.Runtime.CompilerServices;
using Scenes.Context;
using UnityEngine;

namespace Singleton
{
    public abstract class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
        where T : Component, IMonoBehaviourSingletonInitialize<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (ReferenceEquals(_instance, null))
                {
                    _instance = new GameObject().AddComponent<T>();
                    _instance.gameObject.name = typeof(T).Name;

                    _instance.InitSingleton();
                }

                return _instance;
            }
            protected set
            {
                if (!ReferenceEquals(_instance,null)) Destroy(_instance.gameObject);
                _instance = value;
                if (!ReferenceEquals(_instance,null)) DontDestroyOnLoad(_instance);
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(_instance);
            }
            else Destroy(_instance.gameObject);
        }
    }
}