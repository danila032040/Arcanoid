using System;
using UnityEngine;

namespace Scenes.Game.Services.Screens.Interfaces
{
    public interface IScreenService
    {
        event Action<Vector2> ScreenResolutionChanged;
    }
}