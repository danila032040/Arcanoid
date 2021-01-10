using SaveLoader;
using SaveLoader.Interfaces;
using UnityEngine;

namespace Packs
{
    [CreateAssetMenu(fileName = "Pack", menuName = "Levels/Pack", order = 0)]
    public class Pack : ScriptableObject
    {
        [SerializeField] private TextAsset[] _levels;

        private LevelInfo[] _loadedLevels;
        
        private ILevelSaveLoader _levelSaveLoader;

        public void Init(ILevelSaveLoader levelSaveLoader)
        {
            _levelSaveLoader = levelSaveLoader;
        }

        public void LoadLevels()
        {
            _loadedLevels = new LevelInfo[_levels.Length];

            for (int i = 0; i < _levels.Length; ++i)
            {
                _loadedLevels[i] = _levelSaveLoader.LoadLevel(_levels[i]);
            }
        }
    }
}