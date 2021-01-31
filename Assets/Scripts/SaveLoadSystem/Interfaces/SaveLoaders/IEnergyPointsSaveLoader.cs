using SaveLoadSystem.Data;

namespace SaveLoadSystem.Interfaces.SaveLoaders
{
    public interface IEnergyPointsSaveLoader
    {
        void SaveEnergyPoints(EnergyInfo info);
        EnergyInfo LoadEnergyPoints();
    }
}