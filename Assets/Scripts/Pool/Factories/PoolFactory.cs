namespace Scripts.Pool.Factories
{
    using Scripts.Pool.Interfaces;

    public class PoolFactory<T> : IPoolFactory<T> where T : IPoolable, new()
    {
        public T Create()
        {
            return new T();
        }
    }
}
