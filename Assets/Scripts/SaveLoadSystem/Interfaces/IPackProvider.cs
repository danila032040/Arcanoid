using SaveLoadSystem.Data;

namespace SaveLoader
{
    public interface IPackProvider
    {
        PackInfo[] GetPackInfos();
    }
}