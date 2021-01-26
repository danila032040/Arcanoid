using PopUpSystems;
using Scenes.Game.Blocks;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Player;
using Scenes.Game.PopUps;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PopUpsManager : MonoBehaviour
    {
        [SerializeField] private BlocksManager _blocksManager;

        private MainGamePopUp _mainGamePopUp;
        
        private void Awake()
        {
            _mainGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<MainGamePopUp>();
        }

        public ProgressGameView GetProgressGameView() => _mainGamePopUp.GetProgressGameView();
        public HpView GetHpView() => _mainGamePopUp.GetHpView();
    }
}