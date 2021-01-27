using System;
using UnityEngine;

namespace PopUpSystems
{
    public abstract class PopUp : MonoBehaviour
    {
        public event Action<PopUp> Closing;

        public abstract void EnableInput();
        public abstract void DisableInput();

        protected void OnClosing()
        {
            Closing?.Invoke(this);
        }
    }
}