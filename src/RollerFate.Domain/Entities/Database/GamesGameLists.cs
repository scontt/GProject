namespace RollerFate.Domain.Entities.Database;

public class GamesGameList
{
    public int GameId { get; set; }
    public Guid ListId { get; set; }
    public User? User { get; set; }
}
