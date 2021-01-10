using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(fileName = "LocalizationResource", menuName = "Localization/Resource", order = 1)]
    public class LocalizationResource : ScriptableObject
    {
        [SerializeField] private SystemLanguage _locale;
        [SerializeField] private Translation[] _translations;

        public SystemLanguage Locale => _locale;
        public Translation[] Translations => _translations;
    }
}
