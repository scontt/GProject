namespace GProject.Domain.Dto;

public class GameListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
