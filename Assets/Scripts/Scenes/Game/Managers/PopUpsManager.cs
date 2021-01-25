using PopUpSystems;
using Scenes.Game.Blocks;
using Scenes.Game.PopUps;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PopUpsManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BlocksManager _blocksManager;

        private PauseMenuPopUp _pauseMenuPopUp;
        private void Awake()
        {
            _pauseMenuPopUp = PopUpSystem.Instance.ShowPopUp<PauseMenuPopUp>();
        }
    }
}