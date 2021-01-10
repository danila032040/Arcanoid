using Scenes.ChoosePack.Packs;
using UnityEngine;

namespace Scenes.ChoosePack
{
    public class PackManager : MonoBehaviour
    {
        [SerializeField] private Pack[] _packs;


        [SerializeField] private Transform _packsParent;

        private void Start()
        {
            DisplayPacks();
        }

        public void DisplayPacks()
        {
            for (int i = 0; i < _packs.Length; ++i)
            {
                Instantiate(_packs[i], _packsParent);
            }
        }
    }
}