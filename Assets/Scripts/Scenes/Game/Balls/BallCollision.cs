using System;
using Scenes.Game.Bricks;
using Scripts.Scenes.Game.Bricks;
using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Brick brick = other.gameObject.GetComponent<Brick>();
            if (brick != null)
            {
                brick.Health -= 1;
            }
        }
    }
}