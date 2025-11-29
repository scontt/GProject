using GProject.Application.Auth;
using GProject.DataAccess;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Auth;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GProject.Infrastructure.Auth;

public class AuthService(ApplicationContext context, IConfiguration configuration) : IAuthService
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

    public async Task<string?> LoginAsync(AuthData authData)
    {
        if (authData == null) return null;

        var searchedUser = await context.Users.FirstOrDefaultAsync(x => x.Username == authData.Username);
        if (searchedUser == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(authData.Password, searchedUser.Password))
            return null;

        return GenerateJwt(searchedUser);
    }

    private string GenerateJwt(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
