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

        private Vector3 _initialScale;
        
        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        public float Width => this.transform.localScale.x * _collider.size.x;
        public float Height => this.transform.localScale.y * _collider.size.y;

        public Vector3 GetInitialScale() => _initialScale;
        public Vector3 GetCurrentScale() => transform.localScale;
        public void SetScale(Vector3 scale) => transform.localScale = scale;
    }
}