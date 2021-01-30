using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scenes.Shared
{
    [RequireComponent(typeof(Button))]
    public class AnimatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _clickedScaleCoefficient;
        [SerializeField] private float _animationDuration;

        private Button _button;
        private Vector3 _initialScale;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _initialScale = _button.transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                _button.transform.DOScale(_initialScale * _clickedScaleCoefficient, _animationDuration);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _button.transform.DOScale(_initialScale, _animationDuration);
        }
    }
}