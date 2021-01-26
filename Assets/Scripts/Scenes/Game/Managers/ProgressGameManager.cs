using System;
using PopUpSystems;
using Scenes.Game.Balls;
using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class ProgressGameManager : MonoBehaviour
    {
        private int _maxDestroyableBlocksCount = 1;
        
        [SerializeField] private BallsManager _ballsManager;
        [SerializeField] private BlocksManager _blocksManager;
        [SerializeField] private PopUpsManager _popUpsManager;

        public event OnFloatValueChanged ProgressValueChanged;

        private void Awake()
        {
            _blocksManager.BlocksChanged += BlocksManagerOnBlocksChanged;
            _lastProgress = GetCurrentProgress();
        }

        public void RecalculateMaxDestroyableBlocksCount()
        {
            _maxDestroyableBlocksCount = GetDestroyableBlocksCount(_blocksManager.GetBlocks());
        }

        private float _lastProgress;
        private void BlocksManagerOnBlocksChanged(Block[,] blocks)
        {
            
            ChangeBallSpeedOnBlocksCount();
            OnProgressValueChanged(this, _lastProgress, GetCurrentProgress());
            _lastProgress = GetCurrentProgress();
        }
        
        public void ChangeBallSpeedOnBlocksCount()
        {
            Block[,] blocks = _blocksManager.GetBlocks();
            foreach (Ball ball in _ballsManager.GetBalls())
                ball.GetBallMovement().SetCurrentSpeedProgress(GetCurrentProgress());
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

        private float GetCurrentProgress()
        {
            int n = GetDestroyableBlocksCount(_blocksManager.GetBlocks());
            return 1 - Mathf.Clamp01(n * 1f / _maxDestroyableBlocksCount);
        }

        protected virtual void OnProgressValueChanged(object sender, float oldValue, float newValue)
        {
            _popUpsManager.GetProgressGameView().SetProgressGame(newValue);
            
            ProgressValueChanged?.Invoke(sender, oldValue, newValue);
        }
    }
}