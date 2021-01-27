using Context;
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
        }
        private void UnSubscribe()
        {
            _gameStatusManager.ProgressValueChanged -= OnProgressValueChanged;
            _playerManager.HealthValueChanged -= OnHealthValueChanged;

            _popUpsManager.PauseGame -= PopUpsManagerOnGamePause;
            _popUpsManager.UnPauseGame -= PopUpsManagerOnUnPauseGame;
            _popUpsManager.RestartGame -= PopUpsManagerOnRestartGame;
            _popUpsManager.ReturnGame -= PopUpsManagerOnReturnGame;
        }


        private void Start()
        {
            StartGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameWin();
            }
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
            _levelsManager.SaveInfo();

            GameWinInfo gameWinInfo = new GameWinInfo();
            
            _levelsManager.GetCurrentLevel(out gameWinInfo._currentLevelNumber, out gameWinInfo._currentPackNumber);
            _levelsManager.GetNextLevel(out gameWinInfo._nextLevelNumber, out gameWinInfo._nextPackNumber);

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
            StartGame();
        }

        private void PopUpsManagerOnReturnGame()
        {
            Time.timeScale = 0f;
            SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene, OnOtherSceneLoaded);
        }

        private void OnOtherSceneLoaded(AsyncOperation obj)
        {
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