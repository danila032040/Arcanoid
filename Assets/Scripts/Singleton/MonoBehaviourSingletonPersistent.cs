using System;
using System.Runtime.CompilerServices;
using Scenes.Context;
using UnityEngine;

namespace Singleton
{
    public abstract class MonoBehaviourSingletonPersistent<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (ReferenceEquals(_instance,null))
                {
                    if (typeof(T) == typeof(ProjectContext))
                    {
                        _instance = new GameObject().AddComponent<T>();
                        _instance.gameObject.name = typeof(T).Name;
                    }
                    else
                    {
                        T prefab = ProjectContext.Instance.GetPrefabsConfig().GetPrefab<T>();
                        _instance = Instantiate(prefab);
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(this);
            }
            else Destroy(this);
        }
    }
}