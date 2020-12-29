namespace Scripts.Pool.Interfaces
{
    public interface IPoolFactory<T> where T : IPoolable
    {
        T Create();
    }
}
