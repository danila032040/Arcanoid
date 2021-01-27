using PopUpSystems;
using Scenes.Game.PopUps.MainGamePopUps;
using UnityEngine;

namespace Scenes.Game.Managers
{
    public class PopUpsManager : MonoBehaviour
    {

        private MainGamePopUp _mainGamePopUp;
        
        private void Awake()
        {
            _mainGamePopUp = PopUpSystem.Instance.ShowPopUpOnANewLayer<MainGamePopUp>();
        }
        
        

        public MainGamePopUp GetMainGamePopUp() => _mainGamePopUp;
    }
}