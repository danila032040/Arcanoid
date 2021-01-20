using Scenes.Game.Blocks;

namespace SaveLoadSystem.Interfaces.Infos
{
    public interface IBlockLevelInfo
    {
        BlockType[,] Map { get; }
        float BlockHeight { get; }
        float LeftOffset { get; }
        float RightOffset { get; }
        float OffsetBetweenRows { get; }
        float OffsetBetweenCols { get; }
    }
}