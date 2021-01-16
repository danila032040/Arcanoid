using SaveLoadSystem.Data;

namespace SaveLoadSystem.Interfaces.SaveLoaders
{
    public interface IPlayerInfoSaveLoader
    {
        PlayerInfo LoadPlayerInfo();
        void SavePlayerInfo(PlayerInfo info);
    }
}