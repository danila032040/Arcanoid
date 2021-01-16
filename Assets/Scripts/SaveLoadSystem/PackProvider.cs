using SaveLoadSystem.Data;
using UnityEngine;

namespace SaveLoader
{
    [CreateAssetMenu(fileName = "PackProvider", menuName = "Packs/PackProvider", order = 0)]
    public class PackProvider : ScriptableObject, IPackProvider
    {
        [SerializeField] private PackInfo[] _packs;

        public PackInfo[] GetPackInfos() => _packs;
    }
}