using Scenes.Game.Blocks.BoostedBlocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.BoostedBlocks.BallBoostEffects.AngerBall
{
    public class AngerBallBoostEffect : CatchableBoostEffect
    {
        public override void Catch()
        {
            Debug.Log("ANGERBALL!!!");
        }
    }
}