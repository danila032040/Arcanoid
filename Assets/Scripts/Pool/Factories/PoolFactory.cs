using Pool.Interfaces;

namespace Pool.Factories
{
    public class PoolFactory<T> : IPoolFactory<T> where T : IPoolable, new()
    {
        public T Create()
        {
            return new T();
        }
    }
}
