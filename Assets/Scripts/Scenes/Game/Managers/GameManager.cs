using Context;
using EnergySystem;
using SaveLoadSystem;
using SceneLoader;
using Scenes.Game.Balls;
using Scenes.Game.Blocks;
using Scenes.Game.Utils;
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
            _levelsManager.Init(new InfoSaveLoader());
        }

        private void Subscribe()
        {
            _gameStatusManager.ProgressValueChanged += OnProgressValueChanged;
            _playerManager.HealthValueChanged += OnHealthValueChanged;

            _popUpsManager.PauseGame += PopUpsManagerOnGamePause;
            _popUpsManager.UnPauseGame += PopUpsManagerOnUnPauseGame;
            _popUpsManager.RestartGame += PopUpsManagerOnRestartGame;
            _popUpsManager.ReturnGame += PopUpsManagerOnReturnGame;
            _popUpsManager.NextLevelGame += PopUpsManagerOnNextLevelGame;
        }
        
        private void UnSubscribe()
        {
            _gameStatusManager.ProgressValueChanged -= OnProgressValueChanged;
            _playerManager.HealthValueChanged -= OnHealthValueChanged;

            _popUpsManager.PauseGame -= PopUpsManagerOnGamePause;
            _popUpsManager.UnPauseGame -= PopUpsManagerOnUnPauseGame;
            _popUpsManager.RestartGame -= PopUpsManagerOnRestartGame;
            _popUpsManager.ReturnGame -= PopUpsManagerOnReturnGame;
            _popUpsManager.NextLevelGame -= PopUpsManagerOnNextLevelGame;

        }


        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            UnSubscribe();
            
            _blocksManager.DeleteBlocks();
            _ballsManager.DeleteBalls();

            
            _levelsManager.GetCurrentLevel(out int currentLevelNumber, out int currentPackNumber);
            
            _blocksManager.SpawnBlocks(_levelsManager.LoadLevel(currentLevelNumber, currentPackNumber));
            
            _gameStatusManager.Reset();
            _playerManager.Reset();
            
            Subscribe();
        }

        private void GameOver()
        {
            _popUpsManager.GameOver();
        }

        private void GameWin()
        {
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig().GetEnergyPointsForPassingLevel());
                
            _levelsManager.SaveInfo();

            GameWinInfo gameWinInfo = new GameWinInfo();

            int currentLevelNumber;
            int currentPackNumber;
            
            int nextPackNumber;
            int nextLevelNumber;
            
            _levelsManager.GetCurrentLevel(out currentLevelNumber, out currentPackNumber);
            _levelsManager.GetNextLevel(out nextLevelNumber, out nextPackNumber);

            gameWinInfo._currentLevelNumber = currentLevelNumber;
            gameWinInfo._nextLevelNumber = nextLevelNumber;

            gameWinInfo._currentPack = ProjectContext.Instance.GetPackProvider().GetPackInfo(currentPackNumber);
            gameWinInfo._nextPack = ProjectContext.Instance.GetPackProvider().GetPackInfo(nextPackNumber);

            gameWinInfo._enoughEnergy = EnergyManager.Instance.CanPlayLevel();
            
            _popUpsManager.GameWin(gameWinInfo);
        }
        
        private void PopUpsManagerOnGamePause()
        {
            Time.timeScale = 0f;
        }

        private void PopUpsManagerOnUnPauseGame()
        {
            Time.timeScale = 1f;
        }

        private void PopUpsManagerOnRestartGame()
        {
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig().GetEnergyPointsToPlayLevel());
            StartGame();
        }

        private void PopUpsManagerOnReturnGame()
        {
            Time.timeScale = 0f;
            SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene, OnOtherSceneLoaded);
        }
        
        private void PopUpsManagerOnNextLevelGame()
        {
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig().GetEnergyPointsToPlayLevel());
            
            int nextLevelNumber;
            int nextPackNumber;
            
            _levelsManager.GetNextLevel(out nextLevelNumber, out nextPackNumber);
            
            DataProviderBetweenScenes.Instance.SetCurrentLevelNumber(nextLevelNumber);
            DataProviderBetweenScenes.Instance.SetCurrentPackNumber(nextPackNumber);
            
            StartGame();
        }


        private void OnOtherSceneLoaded(AsyncOperation obj)
        {
            Time.timeScale = 1f;
            _popUpsManager.GetMainGamePopUp().Close();
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