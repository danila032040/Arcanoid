namespace SaveLoadSystem.Interfaces.Infos
{
    public interface IEnergyPointsInfo
    {
        int Count { get; set; }
        long LastTimeUpdated { get; set; }
        long TimePassed { get; set; }
    }
}