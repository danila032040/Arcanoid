using System;
using PopUpSystems;
using Scenes.Game.Services.Inputs.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Game.Services.Inputs.Implementations
{
    public class InputServicePopUp : PopUp, IInputService
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
        
        public override void EnableInput()
        {
            _isEnabled = true;
        }

        public override void DisableInput()
        {
            _isEnabled = false;
        }

        private bool _isEnabled;
        private void CheckMouseButtonDown()
        {
            if (_isEnabled && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) OnMouseButtonDown();
        }

        private void CheckMouseButtonUp()
        {
            if (_isEnabled && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) OnMouseButtonUp();
        }


        Vector3Int _oldMousePos;

        private void CheckMousePositionChanged()
        {
            if (!_isEnabled) return;
            Vector3Int currentMousePos = Vector3Int.RoundToInt(Input.mousePosition);
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