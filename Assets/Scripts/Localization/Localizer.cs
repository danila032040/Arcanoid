using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Localization
{
    public class Localizer : MonoBehaviour
    {
        private Dictionary<Locale, Dictionary<string, string>> _words;
        private Locale _locale = Locale.Default;

        public event Action LocaleChanged;

        public Locale Locale
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
            Resources.LoadAll<LocalizationResource>("/");
            _words = new Dictionary<Locale, Dictionary<string, string>>();

            foreach(Locale locale in System.Enum.GetValues(typeof(Locale)))
            {
                _words[locale] = new Dictionary<string, string>();
            }

            foreach(UnityEngine.Object resObj in Resources.FindObjectsOfTypeAll(typeof(LocalizationResource)))
            {
                LocalizationResource res = (LocalizationResource)resObj;

                foreach(Translation translation in res.Translations)
                {
                    _words[res.Locale][translation.Key] = translation.Value;
                }
            }
        }

        public string Localize(string key)
        {
            return _words[_locale][key];
        }
    }
}
