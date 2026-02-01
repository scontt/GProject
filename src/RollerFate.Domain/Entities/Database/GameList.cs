namespace RollerFate.Domain.Entities.Database;

public class GameList
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Game>? Games { get; set; }
    public User? User { get; set; }
}
