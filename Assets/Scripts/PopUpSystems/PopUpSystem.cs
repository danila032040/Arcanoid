using System;
using System.Collections.Generic;
using Scenes.Context;
using Singleton;
using UnityEngine;

namespace PopUpSystems
{
    public class PopUpSystem : MonoBehaviourSingletonPersistent<PopUpSystem>
    {
        private readonly Stack<PopUp> _stack = new Stack<PopUp>();

        [SerializeField] private List<PopUp> _popUpPrefabs;

        private Canvas _canvas;


        public PopUp ShowPopUp(Type type)
        {
            PopUp popUpPrefab = _popUpPrefabs.Find(a => a.GetType() == type);
            PopUp popUp = CreatePopUp(popUpPrefab);
            
            _stack.Peek().DisableInput();
            _stack.Push(popUp);

            return popUp;
        }

        public T ShowPopUp<T>() where T : PopUp
        {
            return ShowPopUp(typeof(T)) as T;
        }
        
        

        private PopUp CreatePopUp(PopUp popUpPrefab)
        {
            if (_canvas == null)
            {
                _canvas = new GameObject().AddComponent<Canvas>();
                _canvas.name = name;
            }
            PopUp popUp = Instantiate(popUpPrefab,_canvas.transform);
            popUp.Closing += PopUpOnOnClosing;
            return popUp;
        }

        private void DeletePopUp(PopUp popUp)
        {
            popUp.Closing -= PopUpOnOnClosing;
            Destroy(popUp.gameObject);
        }

        private void PopUpOnOnClosing(PopUp obj)
        {
            PopUp currentPopUp;
            do
            {
                currentPopUp = _stack.Pop();
                DeletePopUp(currentPopUp);
            } while (currentPopUp != obj);
            
            currentPopUp = _stack.Peek();
            currentPopUp.EnableInput();
        }
    }
}