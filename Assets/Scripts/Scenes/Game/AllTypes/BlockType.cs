namespace Scenes.Game.AllTypes
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
        
        CaptiveBallBoost = 200,
        AngryBallBoost,
        IncreaseBallsSpeedBoost,
        DecreaseBallsSpeedBoost,
        
        IncreasePaddleSpeedBoost = 300,
        DecreasePaddleSpeedBoost,
        IncreasePaddleSizeBoost,
        DecreasePaddleSizeBoost,
        
        AddHealthBoost = 400,
        RemoveHealthBoost
    }
}