using System;
using Context;
using PopUpSystems;
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
        [SerializeField] private GameStatusManager _gameStatusManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private PopUpsManager _popUpsManager;

        private void Awake()
        {
            _gameStatusManager.ProgressValueChanged += OnValueChanged; 
            _playerManager.HealthValueChanged += OnHealthValueChanged;
            
            _popUpsManager.GetMainGamePopUp().ButtonPauseGamePressed += GamePause;
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
            
            _gameStatusManager.Reset();
            _playerManager.Reset();
        }

        public void GameOver()
        {
        }
        
        public void GameWin()
        {
        }
        
        public void GamePause()
        {
            _popUpsManager.GetMainGamePopUp().ButtonPauseGamePressed -= GamePause;
            Time.timeScale = 0f;
        }

        public void GameUnPause()
        {
            Time.timeScale = 1f;
            _popUpsManager.GetMainGamePopUp().ButtonPauseGamePressed += GamePause;
        }

        
        
        private void OnValueChanged(object sender, float oldValue, float newValue)
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