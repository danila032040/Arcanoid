using System.Collections.Generic;
using UnityEngine;

namespace PopUpSystems
{
    public class PopUpSystemLayer
    {
        private List<PopUp> _popUps = new List<PopUp>();

        private bool isEnabled;

        public void DisableInput()
        {
            isEnabled = false;
            foreach (PopUp popUp in _popUps)
            {
                popUp.DisableInput();
            }
        }

        public void EnableInput()
        {
            isEnabled = true;
            foreach (PopUp popUp in _popUps)
            {
                popUp.EnableInput();
            }
        }

        public void Add(PopUp popUp)
        {
            if (isEnabled) popUp.EnableInput();
            else popUp.DisableInput();
            
            _popUps.Add(popUp);
        }

        public void Remove(PopUp popUp)
        {
            popUp.DisableInput();
            _popUps.Remove(popUp);
        }
        public List<PopUp> GetPopUps() => _popUps;
    }
}