namespace GProject.Domain.Entities.Database;

public class GamesList
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<Game>? Games { get; set; }
}
