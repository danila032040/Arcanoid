using UnityEngine;

namespace Scenes
{
    public abstract class MonoBehaviourSingletonPersistent<T> : MonoBehaviour where T : Component
    {
        public static T Instance;
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this);
            }
            else Destroy(this);
        }
    }
}