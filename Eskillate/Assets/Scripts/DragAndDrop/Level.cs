using Core;
using Label = LabelHelper.Label;

public class Level : ILevel
{
    public MiniGameId MiniGameId { get; set; }
    public int LevelId { get; set; }
    public Label NameLabel { get; set; }
    public Label DescriptionLabel { get; set; }
    public int HighScore { get; set; }

    public void Load()
    {}
}
