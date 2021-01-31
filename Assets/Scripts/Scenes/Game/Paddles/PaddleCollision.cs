using Scenes.Game.Blocks.Boosters.Base;
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
            CatchableBoost catchableBoost = triggeredCollider.gameObject.GetComponent<CatchableBoost>();
            if (!catchableBoost) return;
            
            catchableBoost.Use();
        }
    }
}