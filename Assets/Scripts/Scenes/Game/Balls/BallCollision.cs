using Scenes.Game.Blocks;
using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckCollisionWithDestroyableBlock(collision);
        }
        
        private void CheckCollisionWithDestroyableBlock(Collision2D collision)
        {
            DestroyableBlock block = collision.gameObject.GetComponent<DestroyableBlock>();
            if (!block) return;
            
            block.GetBlockDestructibility().AddHealth(-1);
        }
    }
}