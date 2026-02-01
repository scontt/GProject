namespace RollerFate.Infrastructure.Auth;

public class AuthSettings
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public TimeSpan Expires { get; set; }
    public string SecretKey { get; set; } = null!;
}
