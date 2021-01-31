using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Shared.PopUps.MainGamePopUpViews
{
    public class ProgressGameView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetProgressGame(float value)
        {
            _slider.DOValue(value, 0.25f);
        }
    }
}