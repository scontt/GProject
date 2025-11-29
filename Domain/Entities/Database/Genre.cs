namespace GProject.Domain.Entities.Database;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Game>? Games { get; set; }
}
