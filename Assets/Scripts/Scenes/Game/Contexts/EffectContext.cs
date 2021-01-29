using System;
using Scenes.Game.Balls;
using Scenes.Game.Managers;
using Scenes.Game.Paddles;

namespace Scenes.Game.Contexts
{
    public class EffectContext
    {
        public readonly BallsManager BallsManager;
        public readonly GameStatusManager GameStatusManager;
        public readonly Paddle Paddle;

        public EffectContext(BallsManager ballsManager, GameStatusManager gameStatusManager, Paddle paddle)
        {
            BallsManager = ballsManager ? ballsManager : throw new ArgumentNullException(nameof(ballsManager));
            GameStatusManager = gameStatusManager
                ? gameStatusManager
                : throw new ArgumentNullException(nameof(GameStatusManager));
            Paddle = paddle ? paddle : throw new ArgumentNullException(nameof(Paddle));
        }
    }
}