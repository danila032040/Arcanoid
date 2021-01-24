using System;
using Scenes.Game.Services.Inputs.Interfaces;
using UnityEngine;

namespace Scenes.Game.Services.Inputs.Implementations
{
    public class InputService : MonoBehaviour, IInputService
    {
        public event Action MouseButtonDown;
        public event Action MouseButtonUp;
        public event Action<Vector3> MousePositionChanged;

        private void Update()
        {
            CheckMouseButtonUp();
            CheckMouseButtonDown();

            CheckMousePositionChanged();
        }

        private void CheckMouseButtonDown()
        {
            if (Input.GetMouseButtonDown(0)) OnMouseButtonDown();
        }

        private void CheckMouseButtonUp()
        {
            if (Input.GetMouseButtonUp(0)) OnMouseButtonUp();
        }


        Vector3 _oldMousePos;

        private void CheckMousePositionChanged()
        {
            Vector3 currentMousePos = Input.mousePosition;
            if (_oldMousePos != currentMousePos)
            {
                OnMousePositionChanged(currentMousePos);
            }

            _oldMousePos = currentMousePos;
        }

        private void OnMouseButtonDown()
        {
            MouseButtonDown?.Invoke();
        }

        private void OnMouseButtonUp()
        {
            MouseButtonUp?.Invoke();
        }

        private void OnMousePositionChanged(Vector3 obj)
        {
            MousePositionChanged?.Invoke(obj);
        }
    }
}