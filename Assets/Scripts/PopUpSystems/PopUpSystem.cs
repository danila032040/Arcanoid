using System;
using System.Collections.Generic;
using Scenes;
using Singleton;
using UnityEngine;

namespace PopUpSystems
{
    public class PopUpSystem : MonoBehaviourSingletonPersistent<PopUpSystem>
    {
        private Stack<PopUp> _stack = new Stack<PopUp>();

        [SerializeField] private List<PopUp> _popUpPrefabs;

        public PopUp ShowPopUp(Type type)
        {
            PopUp popUpPrefab = _popUpPrefabs.Find(a => a.GetType() == type);
            PopUp popUp = Instantiate(popUpPrefab);
            
            _stack.Push(popUp);
            popUp.OnClosing += PopUpOnOnClosing;
            return popUp;
        }

        private void PopUpOnOnClosing(PopUp obj)
        {
            PopUp currentPopUp;
            do
            {
                currentPopUp = _stack.Pop();
            } while (currentPopUp != obj);
        }
    }
}