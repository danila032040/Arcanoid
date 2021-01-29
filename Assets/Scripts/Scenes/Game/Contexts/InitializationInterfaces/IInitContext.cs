namespace Scenes.Game.Contexts.InitializationInterfaces
{
    public interface IInitContext<T> where T : class
    {
        void Init(T context);
    }
}