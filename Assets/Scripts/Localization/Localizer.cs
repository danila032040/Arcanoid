using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Localization
{
    public class Localizer : MonoBehaviour
    {
        [SerializeField] private List<LocalizationResource> _localizationResources = new List<LocalizationResource>();

        private readonly Dictionary<SystemLanguage, Dictionary<string, string>> _words = new Dictionary<SystemLanguage, Dictionary<string, string>>();

        private SystemLanguage _locale;

        public event Action LocaleChanged;

        public SystemLanguage Locale
        {
            get => _locale;
            set
            {
                _locale = value;
                LocaleChanged?.Invoke();
            }
        }

        private void Awake()
        {
            foreach (SystemLanguage locale in System.Enum.GetValues(typeof(SystemLanguage)))
            {
                _words[locale] = new Dictionary<string, string>();
            }

            foreach (LocalizationResource resource in _localizationResources)
            {
                foreach (Translation translation in resource.Translations)
                {
                    _words[resource.Locale][translation.Key] = translation.Value;
                }
            }
        }

        public string Localize(string key)
        {
            return _words[_locale][key];
        }
    }
}
