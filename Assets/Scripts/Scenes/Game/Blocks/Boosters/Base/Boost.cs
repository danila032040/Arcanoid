using Scenes.Game.Contexts;
using Scenes.Game.Contexts.InitializationInterfaces;
using UnityEngine;

namespace Scenes.Game.Blocks.Boosters.Base
{
    public abstract class Boost : MonoBehaviour, IInitContext<BoostContext>
    {
        protected BoostContext Context { get; private set; }

        public void Init(BoostContext context)
        {
            Context = context;
        }

        public abstract void Use();
    }
}