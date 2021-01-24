using UnityEngine;

namespace PopUpSystems
{
    [System.Serializable]
    public class PopUpSystemData
    {
        [SerializeField] private PopUp _popUp;

        public PopUp GetPopUp() => _popUp;

    }
}