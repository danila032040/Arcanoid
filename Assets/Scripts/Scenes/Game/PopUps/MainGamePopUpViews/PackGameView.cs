using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.PopUps.MainGamePopUpViews
{
    public class PackGameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private Image _packImage;

        public void SetLevelName(string levelName) => _levelNameText.text = levelName;
        public void SetPackImage(Sprite packImage) => _packImage.sprite = packImage;
    }
}