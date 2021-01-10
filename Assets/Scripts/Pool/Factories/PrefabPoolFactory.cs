using Pool.Interfaces;
using UnityEngine;

namespace Pool.Factories
{
    public class PrefabPoolFactory<T> : IPoolFactory<T> where T : MonoBehaviour, IPoolable
    {
        private readonly T _prefab;

        public PrefabPoolFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}
