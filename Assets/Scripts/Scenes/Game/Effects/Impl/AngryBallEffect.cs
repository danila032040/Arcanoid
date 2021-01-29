using System.Collections.Generic;
using System.Linq;
using Context;
using DG.Tweening;
using Scenes.Game.Balls.Base;
using Scenes.Game.Blocks.Base;
using Scenes.Game.Effects.Base;
using UnityEngine;

namespace Scenes.Game.Effects.Impl
{
    public class AngryBallEffect : Effect
    {
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _angryColor;
        [SerializeField] private float _changeColorAnimationDuration;
        [SerializeField] private int _ballsIndexLayer;
        [SerializeField] private int _blocksIndexLayer;

        public override void Enable()
        {
            SetBallsAngryState(Context.BallsManager.GetBalls(), true);
            Context.BallsManager.BallsChanged += SetBallsAngry;
        }

        public override void Disable()
        {
            Context.BallsManager.BallsChanged -= SetBallsAngry;
            SetBallsAngryState(Context.BallsManager.GetBalls(), false);
        }

        private void SetBallsAngry(List<Ball> oldBalls, List<Ball> newBalls)
        {
            SetBallsAngryState(oldBalls.Except(newBalls).ToList(), false);
            SetBallsAngryState(newBalls, true);
        }

        private void OnCollisionWithDestroyableBlock(DestroyableBlock block)
        {
            block.GetBlockDestructibility().SetHealth(ProjectContext.Instance.GetHealthConfig().MinBlockHealthValue);
        }

        private void SetBallsAngryState(List<Ball> balls, bool isAngry)
        {
            if (isAngry)
            {
                
            }
            Physics2D.IgnoreLayerCollision(_ballsIndexLayer, _blocksIndexLayer, isAngry);
            foreach (Ball ball in balls)
            {
                ball.GetBallView().GetSpriteRenderer().DOColor(isAngry ? _angryColor : _startColor,
                    _changeColorAnimationDuration);

                if (isAngry) ball.GetBallCollision().CollisionWithDestroyableBlock += OnCollisionWithDestroyableBlock;
                else ball.GetBallCollision().CollisionWithDestroyableBlock -= OnCollisionWithDestroyableBlock;
            }
        }
    }
}