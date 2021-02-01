using System;
using System.Collections;
using Context;
using DG.Tweening;
using EnergySystem;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.Shared
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _addingTextPrefab;
        [SerializeField] private Vector3 _addingTextStartPosition;
        [SerializeField] private Vector3 _addingTextFinalPosition;
        [SerializeField] private float _addingTextAnimationDuration;

        [SerializeField] private TextMeshProUGUI _energyPointsCountText;
        [SerializeField] private TextMeshProUGUI _timeToRestoreOnePoint;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Color _minusColor;
        [SerializeField] private Color _plusColor;

        private void Start()
        {
            StartCoroutine(UpdateEnergyPointsCoroutine());
            EnergyManager.Instance.EnergyPointsValueChanged += (value, newValue) => UpdateEnergyPoints();
        }

        public IEnumerator ShowEnergyChangesCoroutine(int delta)
        {
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = 10000;
            TextMeshProUGUI text = Instantiate(_addingTextPrefab, transform);
            text.rectTransform.localScale = transform.localScale;

            text.color = delta > 0 ? _plusColor : _minusColor;
            text.text = delta > 0 ? $"+{delta}" : $"{delta}";

            text.rectTransform.DOAnchorPos(_addingTextStartPosition, 0f);
            yield return text.rectTransform.DOAnchorPos(_addingTextFinalPosition, _addingTextAnimationDuration).WaitForCompletion();

            Destroy(text.gameObject);
            _canvas.overrideSorting = false;
        }

        private IEnumerator UpdateEnergyPointsCoroutine()
        {
            while (true)
            {
                UpdateEnergyPoints();

                yield return new WaitForSecondsRealtime(1f);
            }
        }

        private void UpdateEnergyPoints()
        {
            _energyPointsCountText.text = $"{EnergyManager.Instance.GetEnergyPointsCount()}/" +
                                          $"{ProjectContext.Instance.GetEnergyConfig().GetInitialEnergyPoints()}";
            TimeSpan timeSpan = TimeSpan.FromSeconds(EnergyManager.Instance.GetRemainingSecondsToRestoreOnePoint());
            if (timeSpan.Ticks != 0)
            {
                _timeToRestoreOnePoint.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }
            else _timeToRestoreOnePoint.text = "";
        }
    }
}