using SaveLoadSystem.Data;

namespace SaveLoadSystem.Interfaces
{
    public interface IPackProvider
    {
        PackInfo[] GetPackInfos();
        int GetPackNumber(PackInfo packInfo);
        PackInfo GetPackInfo(int packNumber);

        int GetPacksCount();
    }
}