using System;
using System.Collections.Generic;
using Scenes.Context;
using Singleton;
using UnityEngine;

namespace PopUpSystems
{
    public class PopUpSystem : MonoBehaviourSingletonPersistent<PopUpSystem>, IMonoBehaviourSingletonInitialize<PopUpSystem>
    {
        private readonly Stack<PopUpSystemLayer> _layers = new Stack<PopUpSystemLayer>();

        [SerializeField] private List<PopUp> _popUpPrefabs;

        [SerializeField] private Canvas _canvas;

        public void InitSingleton()
        {
            Instance = Instantiate(ProjectContext.Instance.GetPrefabsConfig().GetPrefab<PopUpSystem>());
        }
        
        public PopUp ShowPopUp(Type type)
        {
            if (_layers.Count == 0) return ShowPopUpOnANewLayer(type);

            PopUp popUpPrefab = _popUpPrefabs.Find(a => a.GetType() == type);
            PopUp popUp = CreatePopUp(popUpPrefab);

            _layers.Peek().Add(popUp);

            return popUp;
        }

        public PopUp ShowPopUpOnANewLayer(Type type)
        {
            if (_layers.Count > 0) _layers.Peek().DisableInput();
            
            PopUpSystemLayer layer = new PopUpSystemLayer();
            layer.EnableInput();
            _layers.Push(layer);

            return ShowPopUp(type);
        }


        public T ShowPopUp<T>() where T : PopUp
        {
            return ShowPopUp(typeof(T)) as T;
        }


        public T ShowPopUpOnANewLayer<T>() where T : PopUp
        {
            return ShowPopUpOnANewLayer(typeof(T)) as T;
        }


        private PopUp CreatePopUp(PopUp popUpPrefab)
        {
            PopUp popUp = Instantiate(popUpPrefab, _canvas.transform);
            popUp.Closing += PopUpOnClosing;
            return popUp;
        }

        private void DeletePopUp(PopUp popUp)
        {
            popUp.Closing -= PopUpOnClosing;
            Destroy(popUp.gameObject);
        }

        private void PopUpOnClosing(PopUp popUp)
        {
            PopUpSystemLayer currentLayer = _layers.Pop();
            while (!currentLayer.GetPopUps().Contains(popUp))
            {
                currentLayer = _layers.Pop();
                if (!currentLayer.GetPopUps().Contains(popUp))
                {
                    foreach (PopUp item in currentLayer.GetPopUps())
                    {
                        currentLayer.Remove(item);
                        DeletePopUp(item);
                    }
                }
            }
            
            currentLayer.Remove(popUp);
            DeletePopUp(popUp);

            if (currentLayer.GetPopUps().Count != 0)
            {
                _layers.Push(currentLayer);
            }

        }

        
    }
}