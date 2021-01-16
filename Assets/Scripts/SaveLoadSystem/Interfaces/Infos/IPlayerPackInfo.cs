namespace SaveLoadSystem.Interfaces.Infos
{
    public interface IPlayerPackInfo
    {
        void SetOpenedPacks(bool[] openedPacks);
        bool[] GetOpenedPacks();
    }
}