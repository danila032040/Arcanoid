using System;
using PopUpSystems;
using SaveLoadSystem;
using Scenes.Game.Balls;
using Scenes.Game.Blocks;
using Scenes.Game.Effects;
using Scenes.Game.Managers;
using Scenes.Game.Paddles;
using Scenes.Game.Player;
using Scenes.Game.Services.Cameras.Implementations;
using Scenes.Game.Services.Cameras.Interfaces;
using Scenes.Game.Services.Inputs.Implementations;
using Scenes.Game.Services.Inputs.Interfaces;
using Scenes.Game.Services.Screens.Implementations;
using Scenes.Game.Services.Screens.Interfaces;
using Scenes.Game.Walls;
using UnityEngine;

namespace Scenes.Game.Contexts
{
    public class GameContext : MonoBehaviour
    {
        private InputServicePopUp _inputServicePopUp;
        [SerializeField] private ScreenService _screenService;


        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameStatusManager _gameStatusManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private LevelsManager _levelsManager;

        [SerializeField] private BallsManager _ballsManager;
        [SerializeField] private BlocksManager _blocksManager;
        [SerializeField] private EffectsManager _effectsManager;

        [SerializeField] private Camera _camera;
        [SerializeField] private HpController _hpController;
        [SerializeField] private Paddle _paddle;
        [SerializeField] private OutOfBoundsWall _outOfBoundsWall;

        private readonly CameraService _cameraService = new CameraService();

        private void Awake()
        {
            _inputServicePopUp = PopUpSystem.Instance.SpawnPopUp<InputServicePopUp>();
            
            BoostContext = new BoostContext(_blocksManager, _ballsManager, _effectsManager, _hpController);
            EffectContext = new EffectContext(_ballsManager, _gameStatusManager, _paddle);

            LevelsManager.Init(new PlayerInfoSaveLoader());
        }

        private void OnDestroy()
        {
            if (!ReferenceEquals(_inputServicePopUp.gameObject,null)) _inputServicePopUp.Close();
        }

        public BoostContext BoostContext { get; private set; }
        public EffectContext EffectContext { get; private set; }

        public IInputService InputServicePopUp => _inputServicePopUp;
        public IScreenService ScreenService => _screenService;
        public ICameraService CameraService => _cameraService;

        public GameManager GameManager => _gameManager;
        public GameStatusManager GameStatusManager => _gameStatusManager;
        public PlayerManager PlayerManager => _playerManager;
        public LevelsManager LevelsManager => _levelsManager;

        public BallsManager BallsManager => _ballsManager;
        public BlocksManager BlocksManager => _blocksManager;
        public EffectsManager EffectsManager => _effectsManager;

        public Camera Camera => _camera;
        public HpController HpController => _hpController;
        public Paddle Paddle => _paddle;
        public OutOfBoundsWall OutOfBoundsWall => _outOfBoundsWall;
    }
}