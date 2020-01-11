using System;

namespace Core
{
    public interface ILevel
    {
        MiniGameId MiniGameId { get; set; }
        int LevelId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int HighScore { get; set; }
    }

    [Serializable]
    public enum MiniGameId
    {
        DragAndDrop,
        LowPop
    }
}