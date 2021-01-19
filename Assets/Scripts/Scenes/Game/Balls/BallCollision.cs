using Scenes.Game.Blocks;
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
            Block block = collision.gameObject.GetComponent<Block>();
            if (!block) return;
            
            
            block.Health -= 1;
        }
    }
}