using PopUpSystems;
using Scenes.Game.Blocks;
using Scenes.Game.Blocks.Base;
using Scenes.Game.PopUps;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PopUpsManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BlocksManager _blocksManager;

        private PauseMenuButtonPopUp _pauseMenuButtonPopUp;
        private ProgressGamePopUp _progressGamePopUp;
        private HpPopUp _hpPopUp;

        private void Awake()
        {
            _pauseMenuButtonPopUp = PopUpSystem.Instance.ShowPopUp<PauseMenuButtonPopUp>();
            _progressGamePopUp = PopUpSystem.Instance.ShowPopUp<ProgressGamePopUp>();
            _hpPopUp = PopUpSystem.Instance.ShowPopUp<HpPopUp>();

            _blocksManager.BlocksChanged += BlocksManagerOnBlocksChanged;
        }

        private void BlocksManagerOnBlocksChanged(Block[,] obj)
        {
            _progressGamePopUp.SetProgress(_gameManager.GetCurrentProgress());
        }

        public PauseMenuButtonPopUp GetPauseMenuButtonPopUp() => _pauseMenuButtonPopUp;
        public ProgressGamePopUp GetProgressGamePopUp() => _progressGamePopUp;
        public HpPopUp GetHpPopUp() => _hpPopUp;
    }
}