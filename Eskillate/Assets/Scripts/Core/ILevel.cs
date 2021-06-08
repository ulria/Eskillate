using System;
using Label = LabelHelper.Label;

namespace Core
{
    public interface ILevel
    {
        MiniGameId MiniGameId { get; set; }
        int LevelId { get; set; }
        Label NameLabel { get; set; }
        Label DescriptionLabel { get; set; }
        int HighScore { get; set; }
        Stars GetStarsCount();
    }

    [Serializable]
    public enum MiniGameId
    {
        DragAndDrop,
        LowPop
    }
}