using SaveLoadSystem.Data;

namespace Scenes.Game.Utils
{
    [System.Serializable]
    public class GameWinInfo
    {
        public PackInfo _currentPack;
        public int _currentLevelNumber;
        
        public PackInfo _nextPack;
        public int _nextLevelNumber;

        public bool _enoughEnergy;
    }
}