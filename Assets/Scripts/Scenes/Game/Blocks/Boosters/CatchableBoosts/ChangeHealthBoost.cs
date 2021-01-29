using Scenes.Game.Blocks.Boosters.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.CatchableBoosts
{
    public class ChangeHealthBoost : CatchableBoost
    {
        [SerializeField] private int _addHealth;
        public override void Use()
        {
            Context.HpController.AddHpValue(_addHealth);
            Destroy(gameObject);
        }
    }
}