using System;
using Scenes.Game.Balls;
using Scenes.Game.Blocks;
using Scenes.Game.Effects;
using Scenes.Game.Player;

namespace Scenes.Game.Contexts
{
    public class BoostContext
    {
        public readonly BlocksManager BlocksManager;
        public readonly BallsManager BallsManager;
        public readonly EffectsManager EffectsManager;
        public readonly HpController HpController;

        public BoostContext(BlocksManager blocksManager, BallsManager ballsManager, EffectsManager effectsManager,
            HpController hpController)
        {
            BlocksManager = blocksManager ? blocksManager : throw new ArgumentNullException(nameof(blocksManager));
            BallsManager = ballsManager ? ballsManager : throw new ArgumentNullException(nameof(ballsManager));
            EffectsManager = effectsManager ? effectsManager : throw new ArgumentNullException(nameof(effectsManager));
            HpController = hpController ? hpController : throw new ArgumentNullException(nameof(hpController));
        }
    }
}