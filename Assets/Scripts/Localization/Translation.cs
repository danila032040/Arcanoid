using System;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class Translation
    {
        [SerializeField] private string _key;
        [SerializeField] private string _value;

        public string Key => _key;
        public string Value => _value;
    }
}
