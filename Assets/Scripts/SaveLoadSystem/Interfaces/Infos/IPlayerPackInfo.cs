namespace SaveLoadSystem.Interfaces.Infos
{
    public interface IPlayerPackInfo
    {

        void SetLastPlayedLevels(int[] lastPlayedLevels);
        int[] GetLastPlayedLevels();
        
        void SetOpenedPacks(bool[] openedPacks);
        bool[] GetOpenedPacks();
    }
}