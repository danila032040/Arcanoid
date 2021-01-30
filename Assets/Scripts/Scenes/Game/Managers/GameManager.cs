using Context;
using EnergySystem;
using SaveLoadSystem;
using SceneLoader;
using Scenes.Game.Blocks.Boosters.Base;
using Scenes.Game.Contexts;
using Scenes.Game.Utils;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private void Awake()
        {
            _gameContext.LevelsManager.Init(new InfoSaveLoader());
        }

        private void Subscribe()
        {
            _gameContext.GameStatusManager.ProgressValueChanged += OnProgressValueChanged;
            _gameContext.PlayerManager.HealthValueChanged += OnHealthValueChanged;

            _gameContext.PopUpsManager.PauseGame += PopUpsManagerOnGamePause;
            _gameContext.PopUpsManager.UnPauseGame += PopUpsManagerOnUnPauseGame;
            _gameContext.PopUpsManager.RestartGame += PopUpsManagerOnRestartGame;
            _gameContext.PopUpsManager.ReturnGame += PopUpsManagerOnReturnGame;
            _gameContext.PopUpsManager.NextLevelGame += PopUpsManagerOnNextLevelGame;
        }
        
        private void UnSubscribe()
        {
            _gameContext.GameStatusManager.ProgressValueChanged -= OnProgressValueChanged;
            _gameContext.PlayerManager.HealthValueChanged -= OnHealthValueChanged;

            _gameContext.PopUpsManager.PauseGame -= PopUpsManagerOnGamePause;
            _gameContext.PopUpsManager.UnPauseGame -= PopUpsManagerOnUnPauseGame;
            _gameContext.PopUpsManager.RestartGame -= PopUpsManagerOnRestartGame;
            _gameContext.PopUpsManager.ReturnGame -= PopUpsManagerOnReturnGame;
            _gameContext.PopUpsManager.NextLevelGame -= PopUpsManagerOnNextLevelGame;

        }


        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            UnSubscribe();
            
            _gameContext.BlocksManager.DeleteBlocks();
            _gameContext.BallsManager.DeleteBalls();
            _gameContext.EffectsManager.DeleteEffects();
            
            foreach (CatchableBoost effect in FindObjectsOfType<CatchableBoost>())
            {
                Destroy(effect.gameObject);
            }

            
            _gameContext.LevelsManager.GetCurrentLevel(out int currentLevelNumber, out int currentPackNumber);
            
            _gameContext.BlocksManager.SpawnBlocks(_gameContext.LevelsManager.LoadLevel(currentLevelNumber, currentPackNumber));
            
            _gameContext.GameStatusManager.Reset();
            _gameContext.PlayerManager.Reset();
            
            Subscribe();
        }

        private void GameOver()
        {
            _gameContext.PopUpsManager.GameOver();
        }

        private void GameWin()
        {
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig().GetEnergyPointsForPassingLevel());
                
            _gameContext.LevelsManager.SaveInfo();

            GameWinInfo gameWinInfo = new GameWinInfo();

            int currentLevelNumber;
            int currentPackNumber;
            
            int nextPackNumber;
            int nextLevelNumber;
            
            _gameContext.LevelsManager.GetCurrentLevel(out currentLevelNumber, out currentPackNumber);
            _gameContext.LevelsManager.GetNextLevel(out nextLevelNumber, out nextPackNumber);

            gameWinInfo._currentLevelNumber = currentLevelNumber;
            gameWinInfo._nextLevelNumber = nextLevelNumber;

            gameWinInfo._currentPack = ProjectContext.Instance.GetPackProvider().GetPackInfo(currentPackNumber);
            gameWinInfo._nextPack = ProjectContext.Instance.GetPackProvider().GetPackInfo(nextPackNumber);

            gameWinInfo._enoughEnergy = EnergyManager.Instance.CanPlayLevel();
            
            _gameContext.PopUpsManager.GameWin(gameWinInfo);
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
            
            _gameContext.LevelsManager.GetNextLevel(out nextLevelNumber, out nextPackNumber);
            
            DataProviderBetweenScenes.Instance.SetCurrentLevelNumber(nextLevelNumber);
            DataProviderBetweenScenes.Instance.SetCurrentPackNumber(nextPackNumber);
            
            StartGame();
        }


        private void OnOtherSceneLoaded(AsyncOperation obj)
        {
            Time.timeScale = 1f;
            _gameContext.PopUpsManager.GetMainGamePopUp().Close();
        }


        private void OnProgressValueChanged(float oldValue, float newValue)
        {
            if (Mathf.Approximately(newValue, 1f))
            {
                GameWin();
            }
        }
        private void OnHealthValueChanged(int oldValue, int newValue)
        {
            if (newValue <= ProjectContext.Instance.GetHealthConfig().MinBlockHealthValue)
            {
                GameOver();
            }
        }
        
        
    }
}