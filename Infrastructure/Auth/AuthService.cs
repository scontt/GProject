using GProject.Application.Auth;
using GProject.DataAccess;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Auth;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GProject.Domain.Dto.Auth;

namespace GProject.Infrastructure.Auth;

public class AuthService(ApplicationContext context, IConfiguration configuration, 
    IOptions<AuthSettings> options, IJwtSigningService jwtService) : IAuthService
{
    public async Task<UserDto?> RegisterAsync(AuthData authData)
    {
        if (await context.Users.AnyAsync(u => u.Username == authData.Username))
            return null;

        var user = authData.Adapt<User>();

        user.Id = Guid.NewGuid();
        user.Password = BCrypt.Net.BCrypt.HashPassword(authData.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user.Adapt<UserDto>();
    }

    public async Task<TokenResponse?> LoginAsync(AuthData authData, HttpContext httpContext)
    {
        if (authData == null) return null;

        var searchedUser = await context.Users.FirstOrDefaultAsync(x => x.Username == authData.Username);
        if (searchedUser == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(authData.Password, searchedUser.Password))
            return null;

        var accessToken = GenerateJwt(searchedUser);

        var (refreshPlain, refreshHash, expiresAt) = GenerateRefresh(configuration);

        var refreshEntity = new RefreshToken
        {
            UserId = searchedUser.Id,
            TokenHash = refreshHash,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        context.RefreshTokens.Add(refreshEntity);
        await context.SaveChangesAsync();

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshPlain
        };
    }

    private string GenerateJwt(User user)
    {
        var claims = new List<Claim>()
        {
            new("id", user.Id.ToString()),
            new("username", user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            signingCredentials: jwtService.Credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private (string PlainToken, string TokenHash, DateTime ExpiresAt) GenerateRefresh(IConfiguration configuration)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        string plain = Base64UrlEncoder.Encode(randomBytes);
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(plain));
        string hash = Convert.ToBase64String(hashBytes);

        int days = 30;
        var configured = configuration["JwtSettings:RefreshTokenExpiresInDays"];
        if (int.TryParse(configured, out var parsed) && parsed > 0)
            days = parsed;

        var expiresAt = DateTime.UtcNow.AddDays(days);

        return (plain, hash, expiresAt);
    }

    public async Task<TokenResponse?> RefreshAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken)) return null;

        // hash incoming token
        var incomingHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));

        var dbToken = await context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == incomingHash);
        if (dbToken == null || dbToken.IsRevoked || dbToken.ExpiresAt <= DateTime.UtcNow)
            return null;

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == dbToken.UserId);
        if (user == null) return null;

        // Revoke old token and create new one (rotation)
        dbToken.IsRevoked = true;
        dbToken.RevokedAt = DateTime.UtcNow;

        var (newPlain, newHash, newExpires) = GenerateRefresh(configuration);
        var newEntity = new RefreshToken
        {
            UserId = user.Id,
            TokenHash = newHash,
            ExpiresAt = newExpires,
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        context.RefreshTokens.Add(newEntity);
        await context.SaveChangesAsync();

        var access = GenerateJwt(user);

        return new TokenResponse
        {
            AccessToken = access,
            RefreshToken = newPlain
        };
    }

    public async Task<bool> RevokeRefreshAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken)) return false;

        var incomingHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
        var dbToken = await context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == incomingHash);
        if (dbToken == null || dbToken.IsRevoked) return false;

        dbToken.IsRevoked = true;
        dbToken.RevokedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();

        return true;
    }
}
