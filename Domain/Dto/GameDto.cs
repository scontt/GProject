using GProject.Domain.Entities.Database;
using Mapster;

namespace GProject.Domain.Dto;

public class GameDto : IMapFrom<Game>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? GenreId { get; set; }
}
