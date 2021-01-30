using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private List<EffectCollision> _effectCollisions;

        private readonly List<Effect> _effects = new List<Effect>();
        private readonly Dictionary<EffectType, Coroutine> _effectTypes = new Dictionary<EffectType, Coroutine>();

        private readonly Dictionary<EffectType, Dictionary<EffectType, bool>> _equalEffects =
            new Dictionary<EffectType, Dictionary<EffectType, bool>>();

        public void SpawnEffect(EffectType type)
        {
            StopEffectCoroutine(type);
            if (_equalEffects.TryGetValue(type, out var dictionary))
            {
                foreach (EffectType effectType in dictionary.Keys)
                {
                    StopEffectCoroutine(effectType);
                }
            }

            Effect effect = SpawnOneEffect(type);

            _effectTypes[type] = StartCoroutine(StartedEffectLifeTime(effect));
        }

        public void DeleteEffects()
        {
            StopAllCoroutines();
            foreach (EffectType key in _effectTypes.Keys.ToList())
            {
                _effectTypes[key] = null;
            }

            for (int i = 0; i < _effects.Count; ++i)
            {
                RemoveOneEffect(_effects[0]);
            }
        }


        private void Start()
        {
            foreach (EffectType type in _poolManager.GetAllTypes())
                _effectTypes[type] = null;

            foreach (EffectCollision collision in _effectCollisions)
            {
                if (collision._firstEffectType != collision._secondEffectType)
                {
                    AddEqualEffects(collision._firstEffectType, collision._secondEffectType);
                    AddEqualEffects(collision._secondEffectType, collision._firstEffectType);
                }
            }
        }

        private void AddEqualEffects(EffectType first, EffectType second)
        {
            if (!_equalEffects.ContainsKey(first))
                _equalEffects[first] = new Dictionary<EffectType, bool>();

            _equalEffects[first][second] = true;
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

        private void StopEffectCoroutine(EffectType type)
        {
            Coroutine coroutine = _effectTypes[type];
            if (coroutine != null)
            {
                StopCoroutine(coroutine);

                Effect effectToRemove = _effects.Find((e) => e.Type == type);
                if (effectToRemove != null) RemoveOneEffect(effectToRemove);
            }
        }

        private IEnumerator StartedEffectLifeTime(Effect effect)
        {
            yield return new WaitForSeconds(effect.Duration);
            RemoveOneEffect(effect);
        }
    }

    [System.Serializable]
    public class EffectCollision
    {
        public EffectType _firstEffectType;
        public EffectType _secondEffectType;
    }
}