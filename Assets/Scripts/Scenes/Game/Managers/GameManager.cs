using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using Scenes.Game.Balls;
using Scenes.Game.Balls.Pool;
using Scenes.Game.Blocks;
using Scenes.Game.Paddles;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;

        [SerializeField] private BlocksManager _blocksManager;

        private IInputService _inputService;
        private IPackProvider _packProvider;
        private BallsPool _ballsPool;
        private DataProviderBetweenScenes _dataProvider;

        private void Init(IInputService inputService, IPackProvider packProvider,
            BallsPool ballsPool,
            DataProviderBetweenScenes dataProvider)
        {
            _inputService = inputService;
            _packProvider = packProvider;
            _ballsPool = ballsPool;
            _dataProvider = dataProvider;
        }

        [SerializeField] private PackProvider _packProviderImpl;
        [SerializeField] private InputService _inputServiceImpl;

        [SerializeField] private BallsPool _ballsPoolImpl;
        public void Start()
        {
            Init(_inputServiceImpl, _packProviderImpl, _ballsPoolImpl, DataProviderBetweenScenes.Instance);
            StartGame();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }

        private Ball _startBall;
        private void StartGame()
        {
            _startBall = _ballsPool.Get();
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

            _blocksManager.SpawnBlocks(_levelInfos[_currentLevelInfo]);
        }

        private void LoadNextLevel()
        {
            _blocksManager.DeleteBlocks();
             if (++_currentLevelInfo >= _levelInfos.Length)
            {
                _dataProvider.SetSelectedPackNumber(_dataProvider.GetSelectedPackNumber() + 1);
                LoadPack();
            }
            else
            {
                 _blocksManager.SpawnBlocks(_levelInfos[_currentLevelInfo]);
            }
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