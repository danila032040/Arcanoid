using System;
using UnityEngine;

namespace Scenes.Game.Services.Inputs.Interfaces
{
    public interface IInputService
    {
        event Action MouseButtonDown;
        event Action MouseButtonUp;
        event Action<Vector3> MousePositionChanged;
    }
}