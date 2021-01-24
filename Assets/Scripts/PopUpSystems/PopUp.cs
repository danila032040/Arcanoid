using System;
using UnityEngine;

namespace PopUpSystems
{
    public class PopUp : MonoBehaviour
    {
        public event Action<PopUp> OnClosing;
    }
}