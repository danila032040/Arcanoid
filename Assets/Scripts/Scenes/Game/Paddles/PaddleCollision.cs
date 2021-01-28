using System;
using Scenes.Game.Blocks.BoostedBlocks.Base;
using UnityEngine;

namespace Scenes.Game.Paddles
{
    [RequireComponent(typeof(Collider2D))]
    public class PaddleCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D triggeredCollider)
        {
            CheckTriggerWithCatchableBoostEffect(triggeredCollider);
        }

        private void CheckTriggerWithCatchableBoostEffect(Collider2D triggeredCollider)
        {
            CatchableBoostEffect catchableBoostEffect = triggeredCollider.gameObject.GetComponent<CatchableBoostEffect>();
            if (!catchableBoostEffect) return;
            
            catchableBoostEffect.Catch();
        }
    }
}