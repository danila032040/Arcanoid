namespace Scripts.Pool.Factories
{
    using Scripts.Pool.Interfaces;
    using UnityEngine;

    public class PrefabFactory<T> : IPoolFactory<T> where T : MonoBehaviour, IPoolable
    {
        private readonly T _prefab;

        public PrefabFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}
