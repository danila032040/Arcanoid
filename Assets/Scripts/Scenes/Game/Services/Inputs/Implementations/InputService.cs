using System;
using Scenes.Game.Services.Inputs.Interfaces;
using UnityEngine;

namespace Scenes.Game.Services.Inputs.Implementations
{
    public class InputService : MonoBehaviour, IInputService
    {

        public event Action OnMouseButtonDown;
        public event Action OnMouseButtonUp;
        public event Action<Vector3> OnMousePositionChanged;

        private void Update()
        {
            CheckMouseButtonUp();
            CheckMouseButtonDown();

            CheckMousePositionChanged();
        }

        private void CheckMouseButtonDown()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)) OnMouseButtonDown?.Invoke();
        }

        private void CheckMouseButtonUp()
        {
            if (UnityEngine.Input.GetMouseButtonUp(0)) OnMouseButtonUp?.Invoke();
        }


        Vector3 _oldMousePos;

        private void CheckMousePositionChanged()
        {
            Vector3 currentMousePos = UnityEngine.Input.mousePosition;
            if (_oldMousePos != currentMousePos)
            {
                OnMousePositionChanged?.Invoke(currentMousePos);
            }
            _oldMousePos = currentMousePos;
        }
    }
}
