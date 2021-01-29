using System;
using System.Collections;
using System.Collections.Generic;
using Scenes.Game.Contexts;
using Scenes.Game.Effects.Base;
using Scenes.Game.Effects.Pool;
using UnityEngine;

namespace Scenes.Game.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private EffectsPoolManager _poolManager;
        [SerializeField] private GameContext _gameContext;

        private readonly List<Effect> _effects = new List<Effect>();
        private readonly Dictionary<EffectType, Coroutine> _effectTypes = new Dictionary<EffectType, Coroutine>();

        private void Start()
        {
            foreach (EffectType type in _poolManager.GetAllTypes())
                _effectTypes[type] = null;
        }

        public void SpawnEffect(EffectType type)
        {
            Coroutine coroutine = _effectTypes[type];
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                
                Effect effectToRemove = _effects.Find((e) => e.Type == type);
                if (effectToRemove != null) RemoveOneEffect(effectToRemove);
            }

            Effect effect = SpawnOneEffect(type);

            _effectTypes[type] = StartCoroutine(StartedEffectLifeTime(effect));
        }

        public void DeleteEffects()
        {
            StopAllCoroutines();
            foreach (EffectType key in _effectTypes.Keys)
            {
                _effectTypes[key] = null;
            }
            for (int i = 0; i < _effects.Count; ++i)
            {
                RemoveOneEffect(_effects[0]);
            }
        }

        private Effect SpawnOneEffect(EffectType type)
        {
            Effect effect = _poolManager.Get(type);
            effect.Init(_gameContext.EffectContext);
            _effects.Add(effect);
            effect.Enable();

            return effect;
        }

        private void RemoveOneEffect(Effect effect)
        {
            effect.Disable();
            _effects.Remove(effect);
            _poolManager.Remove(effect);
        }

        private IEnumerator StartedEffectLifeTime(Effect effect)
        {
            yield return new WaitForSeconds(effect.Duration);
            RemoveOneEffect(effect);
        }
    }
}