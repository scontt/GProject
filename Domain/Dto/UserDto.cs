namespace GProject.Domain.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
