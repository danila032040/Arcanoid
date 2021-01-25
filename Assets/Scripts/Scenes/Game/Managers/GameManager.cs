using Configurations;
using PopUpSystems;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.SaveLoaders;
using SceneLoader;
using Scenes.Game.Balls;
using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Paddles;
using Scenes.Game.Player;
using Scenes.Game.PopUps;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scenes.Game.Walls;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private HealthConfiguration _healthConfiguration;


        [SerializeField] private Paddle _paddle;

        [SerializeField] private BlocksManager _blocksManager;
        [SerializeField] private BallsManager _ballsManager;
        
        [SerializeField] private HpController _hp;
        [SerializeField] private OutOfBoundsWall _outOfBoundsWall;

        
        private IInputService _inputService;
        private IPackProvider _packProvider;
        private IPlayerInfoSaveLoader _playerInfoSaveLoader;
        private DataProviderBetweenScenes _dataProvider;

        private void Init(IInputService inputService, IPackProvider packProvider,
            IPlayerInfoSaveLoader playerInfoSaveLoader,
            DataProviderBetweenScenes dataProvider)
        {
            _inputService = inputService;
            _packProvider = packProvider;
            _playerInfoSaveLoader = playerInfoSaveLoader;
            _dataProvider = dataProvider;
        }

        [SerializeField] private PackProvider _packProviderImpl;
        [SerializeField] private InputService _inputServiceImpl;

        public void Start()
        {
            Init(_inputServiceImpl, _packProviderImpl, new InfoSaveLoader(), DataProviderBetweenScenes.Instance);

            _blocksManager.BlocksChanged += BlocksManagerOnBlocksChanged;
            _hp.HealthValueChanged += HpOnHealthValueChanged;
            _outOfBoundsWall.OutOfBounds += OnOutOfBounds;

            ResetGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }

        private void BlocksManagerOnBlocksChanged(Block[,] blocks)
        {
            if (blocks!=null && GetDestroyableBlocksCount(blocks) == 0)
            {
                LoadNextLevel();
            }
            else
            {
                ChangeBallSpeedOnBlocksCount(blocks);
            }
        }

        private void OnOutOfBounds(GameObject obj)
        {
            Ball ball = obj.GetComponent<Ball>();
            if (!ReferenceEquals(ball, null))
            {
                _ballsManager.RemoveBall(ball);
                if (_ballsManager.GetBalls().Count <= 0)
                {
                    AttachBall(_ballsManager.SpawnBall());
                    ChangeBallSpeedOnBlocksCount(_blocksManager.GetBlocks());
                    _hp.AddHpValue(_healthConfiguration.AddHealthToPlayerForLoosingAllBalls);
                }
            }
        }

        private void HpOnHealthValueChanged(object sender, int oldValue, int newValue)
        {
            if (newValue <= _healthConfiguration.MinPlayerHealthValue)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            ResetGame();
        }

        private void ResetGame()
        {
            _blocksManager.DeleteBlocks();
            _ballsManager.DeleteBalls();
            
            AttachBall(_ballsManager.SpawnBall());

            int currPackNumber = _dataProvider.GetSelectedPackNumber();
            int currLvlNumber = _playerInfoSaveLoader.LoadPlayerInfo().GetLastPlayedLevels()[currPackNumber];
            LoadLevel(currLvlNumber);

            _hp.SetHpValue(_healthConfiguration.InitialPlayerHealthValue);
        }

        private int _maxDBlocksCount;


        private void ChangeBallSpeedOnBlocksCount(Block[,] blocks)
        {
            foreach (Ball ball in _ballsManager.GetBalls())
            {
                ball.GetBallMovement().SetCurrentSpeedProgress(GetCurrentProgress());
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



        private LevelInfo[] _levelInfos;

        private void LoadPack(int packNumber)
        {
            packNumber = Mathf.Clamp(packNumber, 0, _packProvider.GetPackInfos().Length - 1);

            _levelInfos = _packProvider.GetPackInfo(packNumber).GetLevelInfos();
        }

        private void SpawnBlocksByLevelInfo(LevelInfo levelInfo)
        {
            _blocksManager.SpawnBlocks(levelInfo);
            _maxDBlocksCount = GetDestroyableBlocksCount(_blocksManager.GetBlocks());
        }

        private void LoadLevel(int levelNumber)
        {
            if (_levelInfos == null)
            {
                int packNumber = _dataProvider.GetSelectedPackNumber();
                LoadPack(packNumber);
                levelNumber = _playerInfoSaveLoader.LoadPlayerInfo().GetLastPlayedLevels()[packNumber];
            }

            levelNumber = Mathf.Clamp(levelNumber, 0, _levelInfos.Length - 1);

            SpawnBlocksByLevelInfo(_levelInfos[levelNumber]);
        }

        private void LoadNextLevel()
        {
            PlayerInfo playerInfo = _playerInfoSaveLoader.LoadPlayerInfo();
            int[] lastPlayedLevels = playerInfo.GetLastPlayedLevels();
            bool[] openedPacks = playerInfo.GetOpenedPacks();
            
            int packNumber = _dataProvider.GetSelectedPackNumber();
            int levelNumber = ++lastPlayedLevels[packNumber];

            if (levelNumber >= _levelInfos.Length)
            {
                lastPlayedLevels[packNumber] = _levelInfos.Length - 1;
                ++packNumber;
                if (packNumber >= _packProvider.GetPackInfos().Length)
                {
                    PassedAllPacks();
                    return;
                }

                openedPacks[packNumber] = true;
                
                playerInfo.SetOpenedPacks(openedPacks);
                _dataProvider.SetSelectedPackNumber(packNumber);
                
                LoadPack(packNumber);
            }

            playerInfo.SetLastPlayedLevels(lastPlayedLevels);
            
            _playerInfoSaveLoader.SavePlayerInfo(playerInfo);
            
            ResetGame();
        }

        private void PassedAllPacks()
        {
            //TODO: Use PopUp.
            Debug.Log("Congratulations!!!");
            SceneLoaderController.Instance.LoadScene(LoadingScene.ChoosePackScene);
        }


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


        public float GetCurrentProgress()
        {
            int n = GetDestroyableBlocksCount(_blocksManager.GetBlocks());
            return 1 - Mathf.Clamp01(n * 1f / _maxDBlocksCount);
        }
        
    }
}