using UnityEngine;

namespace Singleton
{
    public interface IMonoBehaviourSingletonInitialize<T> where T : Component
    {
        void InitSingleton();
    }
}