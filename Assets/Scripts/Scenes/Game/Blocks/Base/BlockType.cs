namespace Scenes.Game.Blocks.Base
{
    [System.Serializable]
    public enum BlockType
    {
        None,
        GraniteBlock = 1,
        WhiteDestroyableBlock = 2,
        YellowDestroyableBlock,
        NormalBomb = 100,
        ColorBomb,
        HorizontalBomb,
        VerticalBomb,
        CaptiveBall = 200,
        
    }
}