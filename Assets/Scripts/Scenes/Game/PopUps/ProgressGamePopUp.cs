using DG.Tweening;
using PopUpSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps
{
    public class ProgressGamePopUp : PopUp
    {
        [SerializeField] private Slider _slider;

        public void SetProgress(float value)
        {
            _slider.DOValue(value, 0.25f);
        }
        public override void DisableInput()
        {
        }

        public override void EnableInput()
        {
        }
    }
}