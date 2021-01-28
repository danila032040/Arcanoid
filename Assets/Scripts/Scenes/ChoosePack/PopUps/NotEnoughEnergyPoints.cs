using System;
using PopUpSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ChoosePack.PopUps
{
    public class NotEnoughEnergyPoints : PopUp
    {
        [SerializeField] private Button _buttonOk;

        private void Awake()
        {
            _buttonOk.onClick.AddListener(OnClosing);
        }

        public override void EnableInput()
        {
            _buttonOk.interactable = true;
        }

        public override void DisableInput()
        {
            _buttonOk.interactable = false;
        }
    }
}