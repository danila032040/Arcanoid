using SaveLoadSystem.Data;
using UnityEngine;

namespace Scenes
{
    public class DataProviderBetweenScenes : MonoBehaviour
    {
        
        public static DataProviderBetweenScenes Instance;
        
        [SerializeField] private PackInfo _selectedPackInfo;
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(this);
        }

        public PackInfo GetSelectedPackInfo() => _selectedPackInfo;
        
        public void SetSelectedPackInfo(PackInfo packInfo)
        {
            _selectedPackInfo = packInfo;
        }
        
    }
}