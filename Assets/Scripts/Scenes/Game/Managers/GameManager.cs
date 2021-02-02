using System.Collections;
using Context;
using EnergySystem;
using PopUpSystems;
using Scenes.Game.Blocks.Boosters.Base;
using Scenes.Game.Contexts;
using Scenes.Game.Utils;
using Scenes.Shared;
using Scenes.Shared.PopUps;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private MainGamePopUp _mainGamePopUp;

        public MainGamePopUp GetMainGamePopUp() => _mainGamePopUp;

        private void Awake()
        {
            _mainGamePopUp = PopUpSystem.Instance.SpawnPopUpOnANewLayer<MainGamePopUp>();
            _mainGamePopUp.Show(this);
        }

        private void Start()
        {
            Time.timeScale = 1f;
            StartGame();
        }

        private void OnDestroy()
        {
            if (!ReferenceEquals(_mainGamePopUp.gameObject, null))
                _mainGamePopUp.Close();
        }

        private void Subscribe()
        {
            _gameContext.GameStatusManager.ProgressValueChanged += OnProgressValueChanged;
            _gameContext.PlayerManager.HealthValueChanged += OnHealthValueChanged;
        }

        private void UnSubscribe()
        {
            _gameContext.GameStatusManager.ProgressValueChanged -= OnProgressValueChanged;
            _gameContext.PlayerManager.HealthValueChanged -= OnHealthValueChanged;
        }


        private void StartGame()
        {
            UnSubscribe();

            _gameContext.EffectsManager.DeleteEffects();
            _gameContext.BlocksManager.DeleteBlocks();
            _gameContext.BallsManager.DeleteBalls();

            foreach (CatchableBoost effect in FindObjectsOfType<CatchableBoost>())
            {
                Destroy(effect.gameObject);
            }


            _gameContext.LevelsManager.GetCurrentLevel(out int currentLevelNumber, out int currentPackNumber);

            _gameContext.BlocksManager.SpawnBlocks(
                _gameContext.LevelsManager.LoadLevel(currentLevelNumber, currentPackNumber));

            _gameContext.GameStatusManager.Reset();
            _gameContext.PlayerManager.Reset();

            Subscribe();
        }

        public IEnumerator GameRestartCoroutine()
        {
            yield return _mainGamePopUp.EnergyView.ShowEnergyChangesCoroutine(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsToPlayLevel());
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsToPlayLevel());
            StartGame();
        }

        private void GameOver()
        {
            PopUpSystem.Instance.SpawnPopUpOnANewLayer<RestartGamePopUp>().Show(this, true);
        }

        private void GameWin()
        {
            StartCoroutine(_mainGamePopUp.EnergyView.ShowEnergyChangesCoroutine(ProjectContext.Instance
                .GetEnergyConfig()
                .GetEnergyPointsForPassingLevel()));
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsForPassingLevel());

            _gameContext.LevelsManager.SaveInfo();

            GameWinInfo gameWinInfo = new GameWinInfo();

            _gameContext.LevelsManager.GetCurrentLevel(out int currentLevelNumber, out int currentPackNumber);
            _gameContext.LevelsManager.GetNextLevel(out int nextLevelNumber, out int nextPackNumber);

            gameWinInfo._currentLevelNumber = currentLevelNumber;
            gameWinInfo._nextLevelNumber = nextLevelNumber;

            gameWinInfo._currentPack = ProjectContext.Instance.GetPackProvider().GetPackInfo(currentPackNumber);
            gameWinInfo._nextPack = ProjectContext.Instance.GetPackProvider().GetPackInfo(nextPackNumber);

            gameWinInfo._enoughEnergy = EnergyManager.Instance.CanPlayLevel();

            PopUpSystem.Instance.SpawnPopUpOnANewLayer<WinGamePopUp>().Show(this, gameWinInfo, true);
        }

        public IEnumerator BuyHeartCoroutine()
        {
            yield return _mainGamePopUp.EnergyView.ShowEnergyChangesCoroutine(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsToByeOneHeart());
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsToByeOneHeart());

            _gameContext.HpController.AddHpValue(1);
        }

        public IEnumerator PlayNextLevelCoroutine()
        {
            yield return _mainGamePopUp.EnergyView.ShowEnergyChangesCoroutine(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsToPlayLevel());
            EnergyManager.Instance.AddEnergyPoints(ProjectContext.Instance.GetEnergyConfig()
                .GetEnergyPointsToPlayLevel());

            _gameContext.LevelsManager.GetNextLevel(out int nextLevelNumber, out int nextPackNumber);

            DataProviderBetweenScenes.Instance.SetCurrentLevelNumber(nextLevelNumber);
            DataProviderBetweenScenes.Instance.SetCurrentPackNumber(nextPackNumber);
            StartGame();
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