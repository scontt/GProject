using Mapster;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Domain.Dto;

public class GameDto : IMapFrom<Game>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? GenreId { get; set; }
}
