using Scenes.Game.Services.Cameras.Interfaces;
using UnityEngine;

namespace Scenes.Game.Services.Cameras.Implementations
{
    public class CameraService : ICameraService
    {
        public float GetWorldPointWidth(Camera camera)
        {
            return (camera.ViewportToWorldPoint(Vector3.right) - camera.ViewportToWorldPoint(Vector3.zero)).magnitude;
        }
    }
}