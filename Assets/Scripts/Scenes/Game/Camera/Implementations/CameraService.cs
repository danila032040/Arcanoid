namespace Scripts.Scenes.Game.Camera.Implementations
{
    using Scripts.Scenes.Game.Camera.Intrefaces;
    using UnityEngine;
    public class CameraService : ICameraService
    {
        public float GetWorldPointWidth(Camera camera)
        {
            return (camera.ViewportToWorldPoint(Vector3.right) - camera.ViewportToWorldPoint(Vector3.zero)).magnitude;
        }
    }
}
