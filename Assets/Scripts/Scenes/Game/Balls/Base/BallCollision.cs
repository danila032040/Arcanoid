using System;
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
            CheckCollisionWithDestroyableBlock(collision);
        }

        private void CheckCollisionWithDestroyableBlock(Collision2D collision)
        {
            DestroyableBlock block = collision.gameObject.GetComponent<DestroyableBlock>();
            if (!block) return;

            if (_isAngryBall)
            {
                block.GetBlockDestructibility().SetHealth(_healthConfiguration.MinPlayerHealthValue);
            }
            else
            {
                block.GetBlockDestructibility().AddHealth(_healthConfiguration.AddHealthToBlockForCollisionWithBall);
            }
        }


        private bool _isAngryBall = false;
        public void SetAngryBall(bool value) =>_isAngryBall = value;
    }
}