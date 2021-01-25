using UnityEngine;

namespace Singleton
{
    [RequireComponent(typeof(MonoBehaviourSingletonPersistentPrefabManager))]
    public abstract class MonoBehaviourSingletonPersistent<T> : MonoBehaviour, ISerializationCallbackReceiver
        where T : Component
    {
        private static T _instance;

        private static T _staticPrefab;
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _staticPrefab = GetComponent<MonoBehaviourSingletonPersistentPrefabManager>().GetPrefab().GetComponent<T>();
        }

        public static T Instance => _instance ? _instance : (_instance = Instantiate(_staticPrefab));

        protected virtual void Awake()
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