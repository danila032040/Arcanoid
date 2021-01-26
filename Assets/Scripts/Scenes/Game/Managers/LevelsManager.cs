using Context;
using SaveLoadSystem.Data;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class LevelsManager : MonoBehaviour
    {
        public int GetLevelsCount(int packNumber)
        {
            return ProjectContext.Instance.GetPackProvider().GetPacksCount();
        }

        public LevelInfo[] LoadPack(int packNumber)
        {
            int packsCount = ProjectContext.Instance.GetPackProvider().GetPacksCount();
            packNumber = Mathf.Clamp(packNumber, 0, packsCount);

            return ProjectContext.Instance
                .GetPackProvider()
                .GetPackInfo(packNumber)
                .GetLevelInfos();
        }

        public LevelInfo LoadLevel(int levelNumber, int packNumber)
        {
            LevelInfo[] currentLevelInfos = LoadPack(packNumber);
            int levelsCount = currentLevelInfos.Length;
            levelNumber = Mathf.Clamp(levelNumber, 0, levelsCount);

            return currentLevelInfos[levelNumber];
        }
    }
}