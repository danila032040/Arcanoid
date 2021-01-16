using SaveLoadSystem.Interfaces.Infos;

namespace SaveLoadSystem.Data
{
    public class PlayerInfo : IPlayerPackInfo
    {
        private bool[] _openedPacks;
        
        public PlayerInfo()
        {
        }

        public PlayerInfo(bool[] openedPacks)
        {
            _openedPacks = openedPacks;
        }

        public void SetOpenedPacks(bool[] openedPacks)
        {
            _openedPacks = openedPacks;
        }

        public bool[] GetOpenedPacks() => _openedPacks;
    }
}