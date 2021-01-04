namespace Scripts.Scenes.Game.Input
{
    using System;
    using UnityEngine;
    public class InputService : MonoBehaviour, IInputService
    {

        public event Action OnMouseButtonDown;
        public event Action OnMouseButtonUp;
        public event Action<Vector3> OnMousePositionChanged; 
        public event Action<Vector2> OnScreenResolutionChanged;

        private void Update()
        {
            CheckMouseButtonUp();
            CheckMouseButtonDown();

            CheckScreenResolutionChanged();
            CheckMousePositionChanged();
        }

        private void CheckMouseButtonDown()
        {
            if (Input.GetMouseButtonDown(0)) OnMouseButtonDown?.Invoke();
        }

        private void CheckMouseButtonUp()
        {
            if (Input.GetMouseButtonUp(0)) OnMouseButtonUp?.Invoke();
        }

        private Vector2 _oldScreenResolution;

        private void CheckScreenResolutionChanged()
        {

            Vector2 currentScreenResolution = new Vector2(Screen.width, Screen.height);
            if (currentScreenResolution != _oldScreenResolution)
            {
                OnScreenResolutionChanged?.Invoke(currentScreenResolution);
            }
            _oldScreenResolution = currentScreenResolution;
        }


        Vector3 _oldMousePos;
        private void CheckMousePositionChanged()
        {
            Vector3 currentMousePos = Input.mousePosition;
            if (_oldMousePos != currentMousePos)
            {
                OnMousePositionChanged?.Invoke(currentMousePos);
            }
            _oldMousePos = currentMousePos;
        }
    }
}
