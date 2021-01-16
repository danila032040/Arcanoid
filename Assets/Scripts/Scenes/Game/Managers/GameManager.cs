using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces.SaveLoaders;
using Scenes.Game.Balls;
using Scenes.Game.Paddles;
using Scripts.Scenes.Game.Bricks;
using Scripts.Scenes.Game.Input;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Ball _startBall;
        [SerializeField] private Paddle _paddle;

        [SerializeField] private BriksManager _briksManager;

        private IInputService _inputService;
        private ILevelInfoSaveLoader _levelInfoSaveLoader;
        private DataProviderBetweenScenes _dataProvider;

        private void Init(IInputService inputService, ILevelInfoSaveLoader levelInfoSaveLoader, DataProviderBetweenScenes dataProvider)
        {
            _inputService = inputService;
            _levelInfoSaveLoader = levelInfoSaveLoader;
            _dataProvider = dataProvider;
        }

        public void Start()
        {
            Init(FindObjectOfType<InputService>(), new InfoSaveLoader(), DataProviderBetweenScenes.Instance);
            StartGame();
        }

        private void StartGame()
        {
            AttachBall();
            LoadPack();
            
        }

        private LevelInfo[] _levelInfos;
        private void LoadPack()
        {
            PackInfo packInfo = _dataProvider.GetSelectedPackInfo();

            _levelInfos = packInfo.GetLevelInfos();
            
            _briksManager.SpawnBricks(_levelInfos[0]);
        }

        private void LoadNextLevel()
        {
            
        }

        private void AttachBall()
        {

            _startBall.GetBallAttachment().AttachTo(_paddle.gameObject);
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
