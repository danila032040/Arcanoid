using System.Collections.Generic;

namespace PopUpSystems
{
    public class PopUpSystemLayer
    {
        private List<PopUp> _popUps = new List<PopUp>();

        private bool _isEnabled;

        public void DisableInput()
        {
            _isEnabled = false;
            foreach (PopUp popUp in _popUps)
            {
                popUp.DisableInput();
            }
        }

        public void EnableInput()
        {
            _isEnabled = true;
            foreach (PopUp popUp in _popUps)
            {
                popUp.EnableInput();
            }
        }

        public void Add(PopUp popUp)
        {
            if (_isEnabled) popUp.EnableInput();
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