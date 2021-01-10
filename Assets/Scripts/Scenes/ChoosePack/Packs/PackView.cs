using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ChoosePack.Packs
{
    public class PackView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _packPassedLevelsInfoText;

        public void SetPackName(string packName)
        {
            _packNameText.text = packName;
        }

        public void SetPassedLevelsInfo(string info)
        {
            _packPassedLevelsInfoText.text = info;
        }
    }
}