using System;
using UnityEngine;

namespace PopUpSystems
{
    public abstract class PopUp : MonoBehaviour
    {
        public event Action<PopUp> Closing;

        public abstract void DisableInput();
        public abstract void EnableInput();

        protected virtual void OnClosing()
        {
            Closing?.Invoke(this);
        }
    }
}