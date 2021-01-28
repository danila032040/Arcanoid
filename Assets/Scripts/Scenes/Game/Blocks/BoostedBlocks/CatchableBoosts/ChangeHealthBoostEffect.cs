using Scenes.Game.Blocks.BoostedBlocks.Base;
using Scenes.Game.Player;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.CatchableBoosts
{
    public class ChangeHealthBoostEffect : CatchableBoostEffect
    {
        [SerializeField] private int _changeHealth;
        
        private HpController _hpController;

        public void Init(HpController hpController)
        {
            _hpController = hpController;
        }
        public override void Catch()
        {
            _hpController.AddHpValue(_changeHealth);
            Destroy(gameObject);
        }
    }
}