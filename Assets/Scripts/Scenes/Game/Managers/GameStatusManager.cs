using SaveLoadSystem.Data;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Contexts;
using Scenes.Game.Utils;
using Scenes.Shared;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameStatusManager : MonoBehaviour
    {
        private int _maxDestroyableBlocksCount = 1;

        [SerializeField] private GameContext _gameContext;

        public event OnValueChanged<float> ProgressValueChanged;

        private void Awake()
        {
            _gameContext.BlocksManager.BlocksChanged += BlocksManagerOnBlocksChanged;
            _lastProgress = GetCurrentProgress();
        }

        public void Reset()
        {
            _maxDestroyableBlocksCount = GetDestroyableBlocksCount(_gameContext.BlocksManager.GetBlocks()); BlocksManagerOnBlocksChanged(_gameContext.BlocksManager.GetBlocks());

            int levelNumber = DataProviderBetweenScenes.Instance.GetCurrentLevelNumber();
            int packNumber = DataProviderBetweenScenes.Instance.GetCurrentPackNumber();

            PackInfo packInfo = _gameContext.LevelsManager.LoadPack(packNumber);
            
            _gameContext.GameManager.GetMainGamePopUp().GetPackGameView().SetLevelName(packInfo.GetLevelInfos()[levelNumber].Name);
            _gameContext.GameManager.GetMainGamePopUp().GetPackGameView().SetPackImage(packInfo.GetPackSprite());
        }

        private float _lastProgress;
        private void BlocksManagerOnBlocksChanged(Block[,] blocks)
        {
            ChangeBallsSpeedOnBlocksCount();
            OnProgressValueChanged(_lastProgress, GetCurrentProgress());
            _lastProgress = GetCurrentProgress();
        }
        
        public void ChangeBallsSpeedOnBlocksCount()
        {
            _gameContext.BallsManager.SetCurrentSpeedProgress(GetCurrentProgress());
        }
        
        private int GetDestroyableBlocksCount(Block[,] blocks)
        {
            if (ReferenceEquals(blocks, null)) return 0;
            int dBlocksCount = 0;
            foreach (Block block in blocks)
            {
                var dBlock = block as DestroyableBlock;

                if (!ReferenceEquals(dBlock, null))
                {
                    ++dBlocksCount;
                }
            }

            return dBlocksCount;
        }

        public float GetCurrentProgress()
        {
            int n = GetDestroyableBlocksCount(_gameContext.BlocksManager.GetBlocks());
            return 1 - Mathf.Clamp01(n * 1f / _maxDestroyableBlocksCount);
        }

        private void OnProgressValueChanged(float oldValue, float newValue)
        {
            _gameContext.GameManager.GetMainGamePopUp().GetProgressGameView().SetProgressGame(newValue);
            
            ProgressValueChanged?.Invoke(oldValue, newValue);
        }
    }
}