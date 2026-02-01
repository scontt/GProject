namespace RollerFate.Domain.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}
