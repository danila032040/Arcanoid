using System;
using UnityEngine;

namespace Scenes.Game.Walls
{
    [RequireComponent(typeof(Collider2D))]
    public class OutOfBoundsWall : MonoBehaviour
    {
        public event Action<GameObject> OutOfBounds;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnOutOfBounds(other.gameObject);
        }

        private void OnOutOfBounds(GameObject obj)
        {
            OutOfBounds?.Invoke(obj);
        }
    }
}