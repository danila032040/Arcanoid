using System;
using Scenes.Game.Services.Screens.Interfaces;
using UnityEngine;

namespace Scenes.Game.Services.Screens.Implementations
{
    public class ScreenService : MonoBehaviour, IScreenService
    {
        public event Action<Vector2> OnScreenResolutionChanged;

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
                OnScreenResolutionChanged?.Invoke(currentScreenResolution);
            }
            _oldScreenResolution = currentScreenResolution;
        }
    }
}