using Core;

public class Level : ILevel
{
    public MiniGameId MiniGameId { get; set; }
    public int LevelId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int HighScore { get; set; }

    public void Load()
    {}
}
