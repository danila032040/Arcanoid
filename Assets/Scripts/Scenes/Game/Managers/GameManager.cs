using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using Scenes.Game.Balls;
using Scenes.Game.Paddles;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scripts.Scenes.Game.Bricks;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Ball _startBall;
        [SerializeField] private Paddle _paddle;

        [SerializeField] private BriksManager _briksManager;

        private IInputService _inputService;
        private IPackProvider _packProvider;
        private DataProviderBetweenScenes _dataProvider;

        private void Init(IInputService inputService, IPackProvider packProvider,
            DataProviderBetweenScenes dataProvider)
        {
            _inputService = inputService;
            _packProvider = packProvider;
            _dataProvider = dataProvider;
        }

        [SerializeField] private PackProvider _packProviderImpl;
        [SerializeField] private InputService _inputServiceImpl;

        public void Start()
        {
            Init(_inputServiceImpl, _packProviderImpl, DataProviderBetweenScenes.Instance);
            StartGame();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }

        private void StartGame()
        {
            AttachBall();
            LoadPack();
        }

        private LevelInfo[] _levelInfos;
        private uint _currentLevelInfo;

        private void LoadPack()
        {
            int packNumber = _dataProvider.GetSelectedPackNumber();

            _levelInfos = _packProvider.GetPackInfo(packNumber).GetLevelInfos();

            _currentLevelInfo = 0;

            _briksManager.SpawnBricks(_levelInfos[_currentLevelInfo]);
        }

        private void LoadNextLevel()
        {
            if (++_currentLevelInfo >= _levelInfos.Length)
            {
                _dataProvider.SetSelectedPackNumber(_dataProvider.GetSelectedPackNumber() + 1);
                LoadPack();
            }

            _briksManager.DeleteBricks();
            _briksManager.SpawnBricks(_levelInfos[_currentLevelInfo]);
        }

        private void AttachBall()
        {
            _startBall.GetBallAttachment().AttachTo(_paddle.transform);
            _inputService.OnMouseButtonUp += DetachBall;
        }

        private void DetachBall()
        {
            _inputService.OnMouseButtonUp -= DetachBall;
            _startBall.GetBallAttachment().Detach();
            _startBall.GetBallMovement().StartMoving();
        }
    }
}