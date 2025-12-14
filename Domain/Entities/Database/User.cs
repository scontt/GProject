namespace GProject.Domain.Entities.Database;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public List<GameList>? GameLists { get; set; }
}
