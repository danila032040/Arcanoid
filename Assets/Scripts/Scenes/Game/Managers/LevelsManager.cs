using Context;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.SaveLoaders;
using Scenes.Shared;
using Unity.Mathematics;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class LevelsManager : MonoBehaviour
    {
        private IPlayerInfoSaveLoader _playerInfoSaveLoader;

        public void Init(IPlayerInfoSaveLoader playerInfoSaveLoader)
        {
            _playerInfoSaveLoader = playerInfoSaveLoader;
        }
        
        public int GetLevelsCount(int packNumber)
        {
            return ProjectContext.Instance.GetPackProvider().GetPacksCount();
        }

        public PackInfo LoadPack(int packNumber)
        {
            int packsCount = ProjectContext.Instance.GetPackProvider().GetPacksCount();
            packNumber = Mathf.Clamp(packNumber, 0, packsCount);

            return ProjectContext.Instance
                .GetPackProvider()
                .GetPackInfo(packNumber);
        }

        public LevelInfo LoadLevel(int levelNumber, int packNumber)
        {
            LevelInfo[] currentLevelInfos = LoadPack(packNumber).GetLevelInfos();
            int levelsCount = currentLevelInfos.Length;
            levelNumber = Mathf.Clamp(levelNumber, 0, levelsCount);

            return currentLevelInfos[levelNumber];
        }

        public void SaveInfo()
        {
            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo();
            bool[] openedPacks = info.GetOpenedPacks();
            int[] lastPlayedLevels = info.GetLastPlayedLevels();

            int currLevelNumber = DataProviderBetweenScenes.Instance.GetCurrentLevelNumber();
            int currPackNumber = DataProviderBetweenScenes.Instance.GetCurrentPackNumber();
            
            GetNextLevel(out int nextLevelNumber, out int nextPackNumber);

            if (currPackNumber < nextPackNumber)
            {
                openedPacks[nextPackNumber] = true;
                lastPlayedLevels[nextPackNumber] = nextLevelNumber;
                
                lastPlayedLevels[currPackNumber] = ++currLevelNumber;
            }
            else
            {
                lastPlayedLevels[nextPackNumber] = math.max(lastPlayedLevels[nextPackNumber], nextLevelNumber);
                if (currPackNumber == nextPackNumber && currLevelNumber == nextLevelNumber)
                {
                    lastPlayedLevels[currPackNumber] = ++currLevelNumber;
                }
            }
            
            _playerInfoSaveLoader.SavePlayerInfo(info);
            
        }

        public void GetCurrentLevel(out int currLevelNumber, out int currPackNumber)
        {
            currLevelNumber = DataProviderBetweenScenes.Instance.GetCurrentLevelNumber();
            currPackNumber = DataProviderBetweenScenes.Instance.GetCurrentPackNumber();
        }
        
        public void GetNextLevel(out int nextLevelNumber, out int nextPackNumber)
        {
            IPackProvider packProvider = ProjectContext.Instance.GetPackProvider();
            GetCurrentLevel(out nextLevelNumber, out nextPackNumber);

            nextLevelNumber = math.clamp(++nextLevelNumber, 0,
                packProvider.GetPackInfo(nextPackNumber).GetLevelsCount());

            if (nextLevelNumber == packProvider.GetPackInfo(nextPackNumber).GetLevelsCount())
            {
                if (nextPackNumber + 1 != packProvider.GetPacksCount())
                {
                    ++nextPackNumber;
                    nextLevelNumber = 0;
                }
                else
                {
                    GetCurrentLevel(out nextLevelNumber,out nextPackNumber);
                }
            }

        }
    }
}