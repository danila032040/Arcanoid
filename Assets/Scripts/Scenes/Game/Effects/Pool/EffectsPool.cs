using Pool.Abstracts;
using Pool.Factories;
using Scenes.Game.AllTypes;
using Scenes.Game.Effects.Base;
using UnityEngine;

namespace Scenes.Game.Effects.Pool
{
    public class EffectsPool : Pool<Effect>
    {
        [SerializeField] private Effect _prefab;

        public EffectType GetEffectType() => _prefab.Type;
        private void Awake()
        {
            Init(new PrefabPoolFactory<Effect>(_prefab));
        }

        protected override void OnPoolEnter(Effect effect)
        { 
            effect.gameObject.SetActive(false);
            effect.transform.SetParent(transform);
        }

        protected override void OnPoolExit(Effect effect)
        {
            effect.gameObject.SetActive(true);
            effect.transform.SetParent(transform);
        }
    }
}