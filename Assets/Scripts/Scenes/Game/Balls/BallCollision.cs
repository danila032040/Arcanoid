using Scenes.Game.Bricks;
using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckCollisionWithBrick(collision);
        }
        
        private void CheckCollisionWithBrick(Collision2D collision)
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            if (!brick) return;
            
            
            brick.Health -= 1;
        }
    }
}