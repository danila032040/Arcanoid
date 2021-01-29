using Pool.Interfaces;
using Scenes.Game.Contexts;
using Scenes.Game.Contexts.InitializationInterfaces;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Scenes.Game.Effects.Base
{
    public abstract class Effect : MonoBehaviour, IPoolable, IInitContext<EffectContext>
    {
        [SerializeField] private float _duration;
        [SerializeField] private EffectType _effectType;
        public float Duration => _duration;
        public EffectType Type => _effectType;

        
        protected EffectContext Context { get; private set; }

        public abstract void Enable();
        public abstract void Disable();
        public void Init(EffectContext context)
        {
            Context = context;
        }
    }
}