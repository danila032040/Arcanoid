using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pool.Abstracts;
using TMPro;
using UnityEngine;

namespace Scenes.Game.Player
{
    [RequireComponent(typeof(Pool<OneHpView>))]
    public class HpView : MonoBehaviour
    {
        private int _health;
        private readonly List<OneHpView> _oneHpViews = new List<OneHpView>();
        
        private Pool<OneHpView> _pool;
        
        [SerializeField] private float _animationDuration;

        private void Awake()
        {
            _pool = this.GetComponent<Pool<OneHpView>>();
        }

        public void SetHealth(int value)
        {
            _health = value;
            
            SetEqualOneHpViewCount();
        }

        public void AddHealth(int value)
        {
            _health += value;
            SetEqualOneHpViewCount();
        }

        private void SetEqualOneHpViewCount()
        {
            while (_oneHpViews.Count < _health)
            {
                AddOneHpView();
            }

            while (_oneHpViews.Count > _health)
            {
                RemoveOneHpView();
            }
        }

        private void RemoveOneHpView()
        {
            StartCoroutine(RemoveOneHpViewAnim());
        }

        private IEnumerator RemoveOneHpViewAnim()
        {
            OneHpView oneHpView = _oneHpViews[0];
            _oneHpViews.RemoveAt(0);

            oneHpView.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.LocalAxisAdd);
            oneHpView.transform.DOScale(0, _animationDuration);

            yield return new WaitForSeconds(1f);
            _pool.Remove(oneHpView);
        }

        private void AddOneHpView()
        {
            StartCoroutine(AddOneHpViewAnim());
        }
        
        private IEnumerator AddOneHpViewAnim()
        {
            OneHpView oneHpView = _pool.Get();
            _oneHpViews.Add(oneHpView);

            oneHpView.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -360), _animationDuration, RotateMode.LocalAxisAdd);
            oneHpView.transform.DOScale(1, _animationDuration);

            yield break;
        }
    }
}