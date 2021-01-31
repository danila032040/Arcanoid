using System;
using Scenes.Game.Services.Screens.Interfaces;
using UnityEngine;

namespace Scenes.Game.Services.Screens.Implementations
{
    public class ScreenService : MonoBehaviour, IScreenService
    {
        public event Action<Vector2> ScreenResolutionChanged;

        private void Update()
        {
            CheckScreenResolutionChanged();
        }

        private Vector2 _oldScreenResolution;

        private void CheckScreenResolutionChanged()
        {
            Vector2 currentScreenResolution = new Vector2(Screen.width, Screen.height);
            if (currentScreenResolution != _oldScreenResolution)
            {
                OnScreenResolutionChanged(currentScreenResolution);
            }

            _oldScreenResolution = currentScreenResolution;
        }

        private void OnScreenResolutionChanged(Vector2 obj)
        {
            ScreenResolutionChanged?.Invoke(obj);
        }
    }
}