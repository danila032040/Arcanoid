
namespace Scripts.Localization
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "LocalizationResource", menuName = "Localization/Resource", order = 1)]
    public class LocalizationResource : ScriptableObject
    {
        [SerializeField] private SystemLanguage _locale;
        [SerializeField] private List<Translation> _translations;

        public SystemLanguage Locale => _locale;
        public List<Translation> Translations => _translations;
    }
}
