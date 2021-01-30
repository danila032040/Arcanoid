using System;
using System.Collections;
using System.Collections.Generic;
using Context;
using EnergySystem;
using TMPro;
using UnityEngine;

namespace Scenes.Shared
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _energyPointsCountText;
        [SerializeField] private TextMeshProUGUI _timeToRestoreOnePoint;

        private void Start()
        {
            StartCoroutine(UpdateEnergyPoints());
        }

        private IEnumerator UpdateEnergyPoints()
        {
            while (true)
            {
                _energyPointsCountText.text = $"{EnergyManager.Instance.GetEnergyPointsCount()}/" +
                                              $"{ProjectContext.Instance.GetEnergyConfig().GetInitialEnergyPoints()}";
                TimeSpan timeSpan = TimeSpan.FromSeconds(EnergyManager.Instance.GetRemainingSecondsToRestoreOnePoint());
                _timeToRestoreOnePoint.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                yield return new WaitForSecondsRealtime(1f);
            }
        }

    }
}