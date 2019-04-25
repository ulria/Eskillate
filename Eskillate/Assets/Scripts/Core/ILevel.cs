namespace Core
{
    public interface ILevel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int HighScore { get; set; }
    }
}