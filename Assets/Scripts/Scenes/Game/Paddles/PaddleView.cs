using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Scenes.Game.Paddles
{
    public class PaddleView : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _normalScale = transform.localScale;
        }

        public float Width => this.transform.localScale.x * _collider.size.x;
        public float Height => this.transform.localScale.y * _collider.size.y;
        
        
        [SerializeField] private Vector3 _increasedScale;
        [SerializeField] private Vector3 _decreasedScale;
        private Vector3 _normalScale;

        private Coroutine _incDecSizeCoroutine;

        public void IncreasePaddleSize(float duration)
        {
            if (_incDecSizeCoroutine != null)
            {
                StopCoroutine(_incDecSizeCoroutine);
                _incDecSizeCoroutine = null;
            }
            _incDecSizeCoroutine = StartCoroutine(ChangePaddleSizeForDuration(_normalScale, _increasedScale, duration));
        }
        public void DecreasePaddleSize(float duration)
        {
            if (_incDecSizeCoroutine != null)
            {
                StopCoroutine(_incDecSizeCoroutine);
                _incDecSizeCoroutine = null;
            }
            _incDecSizeCoroutine = StartCoroutine(ChangePaddleSizeForDuration(_normalScale, _decreasedScale, duration));
        }

        [SerializeField] private float _changePaddleSizeAnimationDuration;

        private IEnumerator ChangePaddleSizeForDuration(Vector3 initialScale, Vector3 scale, float duration)
        {
            yield return transform.DOScale(scale, _changePaddleSizeAnimationDuration);
            yield return new WaitForSeconds(duration);
            yield return transform.DOScale(initialScale, _changePaddleSizeAnimationDuration);
        }
    }
}