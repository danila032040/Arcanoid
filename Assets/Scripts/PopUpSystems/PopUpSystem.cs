using System;
using System.Collections.Generic;
using Context;
using Singleton;
using UnityEngine;

namespace PopUpSystems
{
    public class PopUpSystem : MonoBehaviourSingletonPersistent<PopUpSystem>,
        IMonoBehaviourSingletonInitialize<PopUpSystem>
    {
        private readonly Stack<PopUpSystemLayer> _layers = new Stack<PopUpSystemLayer>();

        [SerializeField] private List<PopUp> _popUpPrefabs;

        [SerializeField] private Canvas _canvas;

        public void InitSingleton()
        {
            Instance = null;
            Instantiate(ProjectContext.Instance.GetPrefabsConfig().GetPrefab<PopUpSystem>());
        }

        public PopUp SpawnPopUp(Type type)
        {
            if (_layers.Count == 0) return SpawnPopUpOnANewLayer(type);

            PopUp popUpPrefab = _popUpPrefabs.Find(a => a.GetType() == type);
            PopUp popUp = CreatePopUp(popUpPrefab);

            _layers.Peek().Add(popUp);

            return popUp;
        }

        public PopUp SpawnPopUpOnANewLayer(Type type)
        {
            if (_layers.Count > 0) _layers.Peek().DisableInput();

            PopUpSystemLayer layer = new PopUpSystemLayer();
            layer.EnableInput();
            _layers.Push(layer);

            return SpawnPopUp(type);
        }


        public T SpawnPopUp<T>() where T : PopUp
        {
            return SpawnPopUp(typeof(T)) as T;
        }


        public T SpawnPopUpOnANewLayer<T>() where T : PopUp
        {
            return SpawnPopUpOnANewLayer(typeof(T)) as T;
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
                if (!currentLayer.GetPopUps().Contains(popUp))
                {
                    while (currentLayer.GetPopUps().Count > 0)
                    {
                        PopUp item = currentLayer.GetPopUps()[0];
                        currentLayer.Remove(item);
                        DeletePopUp(item);
                    }
                }

                currentLayer = _layers.Pop();
            }

            currentLayer.Remove(popUp);
            DeletePopUp(popUp);

            if (currentLayer.GetPopUps().Count != 0)
            {
                _layers.Push(currentLayer);
            }

            if (_layers.Count != 0)
            {
                currentLayer = _layers.Peek();
                currentLayer.EnableInput();
            }
        }
    }
}