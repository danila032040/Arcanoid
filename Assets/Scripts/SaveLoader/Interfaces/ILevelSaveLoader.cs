namespace Scripts.SaveLoader.Interfaces
{
    public interface ILevelSaveLoader
    {
        LevelInfo LoadLevel(string name);
        
        void SaveLevel(LevelInfo info);
    }
}