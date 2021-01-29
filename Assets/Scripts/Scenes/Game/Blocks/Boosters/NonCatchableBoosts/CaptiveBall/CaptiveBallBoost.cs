using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks.Boosters.Base;

namespace Scenes.Game.Blocks.Boosters.NonCatchableBoosts.CaptiveBall
{
    public class CaptiveBallBoost : Boost
    {
        public override void Use()
        {
            Ball ball = Context.BallsManager.SpawnBall();
            ball.transform.position = transform.position;
            ball.GetBallMovement().StartMoving();
        }
    }
}