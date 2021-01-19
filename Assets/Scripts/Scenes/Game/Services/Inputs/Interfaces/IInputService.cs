using System;
using Scenes.Game.Services.Screens.Interfaces;
using UnityEngine;

namespace Scenes.Game.Services.Inputs.Interfaces
{
    public interface IInputService
    {
        event Action OnMouseButtonDown;
        event Action OnMouseButtonUp;
        event Action<Vector3> OnMousePositionChanged;
    }
}