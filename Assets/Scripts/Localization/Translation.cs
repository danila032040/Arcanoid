namespace Scripts.Localization
{
    [System.Serializable]
    public class Translation
    {
        [UnityEngine.SerializeField] private string _key;
        [UnityEngine.SerializeField] private string _value;

        public string Key => _key;
        public string Value => _value;
    }
}
