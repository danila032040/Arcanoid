using Scenes.Game.Balls;
using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks.BoostedBlocks.Base;

namespace Scenes.Game.Blocks.BoostedBlocks.BallBoostEffects.CaptiveBall
{
    public class CaptiveBallBoostEffect : BoostEffect
    {
        private BallsManager _ballsManager;
        
        public void Init(BallsManager ballsManager)
        {
            _ballsManager = ballsManager;
        }
        
        public override void Use()
        {
            Ball ball = _ballsManager.SpawnBall();
            ball.transform.position = transform.position;
            ball.GetBallMovement().StartMoving();
        }
    }
}