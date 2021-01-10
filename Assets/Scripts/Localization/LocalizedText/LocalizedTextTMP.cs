using System;
using TMPro;
using UnityEngine;

namespace Localization.LocalizedText
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextTMP : MonoBehaviour
    {
        [SerializeField] private string _key;
        
        private TextMeshProUGUI _text;

        private Localizer _localizer;

        public void Init(Localizer localizer)
        {
            _localizer = localizer;
        }

        private void Start()
        {
            Init(FindObjectOfType<Localizer>());
            
            _text = this.GetComponent<TextMeshProUGUI>();
            _localizer.LocaleChanged += Localize;
            
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