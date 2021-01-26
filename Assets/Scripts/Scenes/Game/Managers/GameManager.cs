using System;
using Context;
using Scenes.Game.Balls;
using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks;
using Scenes.Game.Paddles;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scenes.Game.Walls;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BlocksManager _blocksManager;
        [SerializeField] private BallsManager _ballsManager;
        [SerializeField] private LevelsManager _levelsManager;
        [SerializeField] private ProgressGameManager _progressGameManager;
        [SerializeField] private PlayerManager _playerManager;

        private void Awake()
        {
            _progressGameManager.ProgressValueChanged += OnProgressValueChanged; 
            _playerManager.HealthValueChanged += OnHealthValueChanged;
        }

        

        private void Start()
        {
            StartGame();
        }




        public void StartGame()
        {
            _blocksManager.DeleteBlocks();
            _ballsManager.DeleteBalls();

            int currentLevelNumber = DataProviderBetweenScenes.Instance.GetCurrentLevelNumber();
            int currentPackNumber = DataProviderBetweenScenes.Instance.GetCurrentPackNumber();

            _blocksManager.SpawnBlocks(_levelsManager.LoadLevel(currentLevelNumber, currentPackNumber));
            
            _progressGameManager.RecalculateMaxDestroyableBlocksCount();
            _playerManager.Reset();
        }

        public void GameOver()
        {
        }
        
        public void GameWin()
        {
        }
        
        private void OnProgressValueChanged(object sender, float oldValue, float newValue)
        {
            if (Mathf.Approximately(newValue, 1f))
            {
                GameWin();
            }
        }
        private void OnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            if (newValue <= ProjectContext.Instance.GetHealthConfig().MinPlayerHealthValue)
            {
                GameOver();
            }
        }
        
        
    }
}