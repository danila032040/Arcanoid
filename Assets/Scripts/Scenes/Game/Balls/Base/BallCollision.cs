using Configurations;
using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Balls.Base
{
    public class BallCollision : MonoBehaviour
    {
        [SerializeField] private HealthConfiguration _healthConfiguration;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckCollisionWithBall(collision);
            CheckCollisionWithDestroyableBlock(collision);
        }

        private void CheckCollisionWithBall(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (!ball) return;
            
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }

        private void CheckCollisionWithDestroyableBlock(Collision2D collision)
        {
            DestroyableBlock block = collision.gameObject.GetComponent<DestroyableBlock>();
            if (!block) return;

            block.GetBlockDestructibility().AddHealth(_healthConfiguration.AddHealthToBlockForCollisionWithBall);
        }
    }
}