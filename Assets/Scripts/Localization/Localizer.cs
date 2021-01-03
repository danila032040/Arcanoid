namespace Scripts.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Localizer : MonoBehaviour
    {
        [SerializeField] private List<LocalizationResource> _localizationResources = new List<LocalizationResource>();

        private readonly Dictionary<string, string> _words = new Dictionary<string, string>();

        private SystemLanguage _locale;

        public event Action LocaleChanged;

        public SystemLanguage Locale
        {
            get => _locale;
            set
            {
                if (_localizationResources.Select(a => a.Locale).Any(a => a == value))
                {
                    _locale = value;
                }
                else
                {
                    SetSystemLocale();
                }

                ReloadDictionary();
                LocaleChanged?.Invoke();
            }
        }

        private void Awake()
        {
            SetSystemLocale();
            ReloadDictionary();
        }

        private void SetSystemLocale()
        {
            _locale = Application.systemLanguage;
        }

        private void ReloadDictionary()
        {
            _words.Clear();
            foreach (LocalizationResource resource in _localizationResources)
                if (resource.Locale == this._locale)
                    foreach (Translation translation in resource.Translations)
                    {
                        _words[translation.Key] = translation.Value;
                    }
        }

        public string Localize(string key)
        {
            return _words[key];
        }

    }
}
