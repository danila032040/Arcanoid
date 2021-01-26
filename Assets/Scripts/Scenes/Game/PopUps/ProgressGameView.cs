using DG.Tweening;
using PopUpSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
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