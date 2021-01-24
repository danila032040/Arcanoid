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
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<T>();
                    _instance.gameObject.name = typeof(T).Name;
                }

                return _instance;
            }
        }
        public void Awake()
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