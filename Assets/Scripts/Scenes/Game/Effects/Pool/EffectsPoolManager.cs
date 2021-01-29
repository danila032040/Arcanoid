using System;
using System.Collections.Generic;
using System.Linq;
using Scenes.Game.Effects.Base;
using UnityEngine;

namespace Scenes.Game.Effects.Pool
{
    public class EffectsPoolManager : MonoBehaviour
    {
        [SerializeField] private EffectsPool[] _pools;

        private readonly Dictionary<EffectType, EffectsPool> _dictionary = new Dictionary<EffectType, EffectsPool>();
        private void Awake()
        {
            foreach (EffectsPool pool in _pools)
            {
                _dictionary[pool.GetEffectType()] = pool;
            }
        }

        public Effect Get(EffectType type) => GetPool(type).Get();

        public void Remove(Effect effect) => GetPool(effect.Type).Remove(effect);

        public List<EffectType> GetAllTypes() => _dictionary.Keys.ToList();
        private EffectsPool GetPool(EffectType type) => _dictionary[type];
        
    }
}