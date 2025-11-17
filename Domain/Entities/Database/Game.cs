namespace GProject.Domain.Entities.Database;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int GenreId { get; set; }
    public Genre? GameGenre { get; set; } = null!;
    public List<GamesList>? Lists { get; set; }
}
