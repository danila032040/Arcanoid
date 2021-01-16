using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ChoosePack.Packs
{
    public class PackView : MonoBehaviour
    {
        [SerializeField] private Color _hideColor;
        [SerializeField] private Color _showColor;
        
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _backgroundImage;

        [SerializeField] private Image _packImage;
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

        public void SetPackSprite(Sprite sprite)
        {
            _packImage.sprite = sprite;
        }

        public void Hide()
        {
            HideAnim(0f);
        }

        public void HideAnim()
        {
            HideAnim(1f);
        }

        private void HideAnim(float duration)
        {
            _backgroundImage.DOColor(_hideColor, duration);
            _canvasGroup.DOFade(.5f, duration);
            _canvasGroup.interactable = false;
        }

        public void Show()
        {
            ShowAnim(0f);
        }

        public void ShowAnim()
        {
            ShowAnim(1f);
        }

        private void ShowAnim(float duration)
        {
            _backgroundImage.DOColor(_showColor, duration);
            _canvasGroup.DOFade(1f, duration);
            _canvasGroup.interactable = true;
        }
    }
}