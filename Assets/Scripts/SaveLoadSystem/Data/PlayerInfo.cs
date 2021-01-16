using SaveLoadSystem.Interfaces.Infos;

namespace SaveLoadSystem.Data
{
    public class PlayerInfo : IPlayerPackInfo
    {
        private bool[] _openedPacks;
        private int[] _lastPlayedLevels;
        
        public PlayerInfo()
        {
        }

        public PlayerInfo(bool[] openedPacks, int[] lastPlayedLevels)
        {
            _openedPacks = openedPacks;
            _lastPlayedLevels = lastPlayedLevels;
        }

        public void SetLastPlayedLevels(int[] lastPlayedLevels)
        {
            _lastPlayedLevels = lastPlayedLevels;
        }

        public int[] GetLastPlayedLevels()
        {
            return _lastPlayedLevels;
        }

        public void SetOpenedPacks(bool[] openedPacks)
        {
            _openedPacks = openedPacks;
        }

        public bool[] GetOpenedPacks() => _openedPacks;
    }
}