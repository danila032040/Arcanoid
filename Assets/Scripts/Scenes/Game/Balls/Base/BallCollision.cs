using System;
using Configurations;
using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Balls.Base
{
    public class BallCollision : MonoBehaviour
    {
        [SerializeField] private HealthConfiguration _healthConfiguration;

        public event Action<DestroyableBlock> CollisionWithDestroyableBlock;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckCollisionWithDestroyableBlock(collision.collider);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            CheckCollisionWithDestroyableBlock(collider);
        }

        private void CheckCollisionWithDestroyableBlock(Collider2D collision)
        {
            DestroyableBlock block = collision.gameObject.GetComponent<DestroyableBlock>();
            if (!block) return;
            block.GetBlockDestructibility().AddHealth(_healthConfiguration.AddHealthToBlockForCollisionWithBall);
            OnCollisionWithDestroyableBlock(block);
        }

        private void OnCollisionWithDestroyableBlock(DestroyableBlock block)
        {
            CollisionWithDestroyableBlock?.Invoke(block);
        }
    }
}