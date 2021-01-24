using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using Scenes.Game.Balls;
using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Paddles;
using Scenes.Game.Player;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scenes.Game.Walls;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;

        [SerializeField] private BlocksManager _blocksManager;
        [SerializeField] private BallsManager _ballsManager;
        [SerializeField] private HpController _hp;
        [SerializeField] private OutOfBoundsWall _outOfBoundsWall;

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
            _blocksManager.BlocksChanged += ChangedSpeedOnBlocksCount;
            _hp.HealthValueChanged += HpOnHealthValueChanged;
            _outOfBoundsWall.OutOfBounds += obj =>
            {
                Ball ball = obj.GetComponent<Ball>();
                if (!ReferenceEquals(ball, null))
                {
                    _ballsManager.RemoveBall(ball);
                    --_ballsInSceneCount;
                    if (_ballsInSceneCount <= 0)
                    {
                        _hp.AddHpValue(-1);
                        AttachBall(_ballsManager.SpawnBall());
                        ChangedSpeedOnBlocksCount(_blocksManager.GetBlocks());
                    }
                }
            };
            StartGame();
        }

        private void HpOnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            if (newValue <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }

        private int _ballsInSceneCount;

        private int _maxDBlocksCount;

        private void StartGame()
        {
            _ballsInSceneCount = 1;
            AttachBall(_ballsManager.SpawnBall());
            LoadPack();
            _hp.SetHpValue(3);
        }

        private void ChangedSpeedOnBlocksCount(Block[,] blocks)
        {
            int n = GetDestroyableBlocksCount(blocks);
            foreach (Ball ball in _ballsManager.GetBalls())
            {
                ball.GetBallMovement().SetCurrentSpeedProgress(1 - n * 1f / _maxDBlocksCount);
            }
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

        

        #region Temporary
        
        private LevelInfo[] _levelInfos;
        private uint _currentLevelInfo;

        private void LoadPack()
        {
            int packNumber = _dataProvider.GetSelectedPackNumber();

            _levelInfos = _packProvider.GetPackInfo(packNumber).GetLevelInfos();

            _currentLevelInfo = 0;

            _blocksManager.SpawnBlocks(_levelInfos[_currentLevelInfo]);
            _maxDBlocksCount = GetDestroyableBlocksCount(_blocksManager.GetBlocks());
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
                _maxDBlocksCount = GetDestroyableBlocksCount(_blocksManager.GetBlocks());
            }
        }

        #endregion

        private Ball _attachedBall;
        private void AttachBall(Ball ball)
        {
            _attachedBall = ball;
            _attachedBall.GetBallAttachment().AttachTo(_paddle.transform);
            _inputService.MouseButtonUp += DetachBall;
        }

        private void DetachBall()
        {
            _inputService.MouseButtonUp -= DetachBall;
            _attachedBall.GetBallAttachment().Detach();
            _attachedBall.GetBallMovement().StartMoving();
        }
    }
}