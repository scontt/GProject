using Microsoft.IdentityModel.Tokens;

namespace RollerFate.Application.Auth;

public interface IJwtSigningService
{
    SigningCredentials Credentials { get; }
    ECDsaSecurityKey PublicKey { get; }
    public IReadOnlyList<SecurityKey> ValidationKeys { get; }
    public string CurrentKeyId { get; }
}
