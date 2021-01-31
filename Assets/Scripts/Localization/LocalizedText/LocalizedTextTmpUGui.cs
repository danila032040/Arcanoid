using TMPro;
using UnityEngine;

namespace Localization.LocalizedText
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextTmpUGui : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private Localizer _localizer;
        
        private TextMeshProUGUI _text;
        
        private void Awake()
        {
            
            _text = this.GetComponent<TextMeshProUGUI>();
            _localizer.LocaleChanged += Localize;
            
            Localize();
        }

        public void SetKey(string key)
        {
            _key = key;
            Localize();
        }

        private void OnDestroy()
        {
            _localizer.LocaleChanged -= Localize;
        }

        private void Localize()
        {
            _text.text = _localizer.Localize(_key);
        }
    }
}