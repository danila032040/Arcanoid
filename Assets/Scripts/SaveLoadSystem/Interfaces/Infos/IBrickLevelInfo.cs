using Scenes.Game.Bricks;

namespace SaveLoadSystem.Interfaces.Infos
{
    public interface IBrickLevelInfo
    {
        BrickType?[,] Map { get; }
        float BrickHeight { get; }
        float LeftOffset { get; }
        float RightOffset { get; }
        float OffsetBetweenRows { get; }
        float OffsetBetweenCols { get; }
    }
}