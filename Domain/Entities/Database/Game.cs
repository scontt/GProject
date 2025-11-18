using GProject.Domain.Dto;
using Mapster;

namespace GProject.Domain.Entities.Database;

public class Game : IMapFrom<GameDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int GenreId { get; set; }
    public Genre? GameGenre { get; set; } = null!;
    public List<GamesList>? Lists { get; set; }
}
