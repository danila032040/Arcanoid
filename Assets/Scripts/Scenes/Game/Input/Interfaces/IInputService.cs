namespace Scripts.Scenes.Game.Input
{
    using System;
    using UnityEngine;
    public interface IInputService
    {
        event Action OnMouseButtonDown;
        event Action OnMouseButtonUp;
        event Action<Vector3> OnMousePositionChanged;
        event Action<Vector2> OnScreenResolutionChanged;
    }
}