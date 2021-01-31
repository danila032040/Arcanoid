using SaveLoadSystem.Interfaces.Infos;
using UnityEngine;

namespace SaveLoadSystem.Data
{
    [System.Serializable]
    public class PlayerInfo : IPlayerPackInfo
    {
        [SerializeField] private bool[] _openedPacks;
        [SerializeField] private int[] _lastPlayedLevels;
        
        public PlayerInfo()
        {
        }

        public PlayerInfo(bool[] openedPacks, int[] lastPlayedLevels)
        {
            _openedPacks = openedPacks;
            _lastPlayedLevels = lastPlayedLevels;
        }
        
        public static PlayerInfo GetDefault(int packsCount)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            bool[] openedPacks = new bool[packsCount];
            openedPacks[0] = true;
            int[] lastPlayedLevels = new int[packsCount];
            
            playerInfo.SetOpenedPacks(openedPacks);
            playerInfo.SetLastPlayedLevels(lastPlayedLevels);
            
            return playerInfo;
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