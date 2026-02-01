using Microsoft.AspNetCore.Http;
using RollerFate.Domain.Dto;
using RollerFate.Domain.Dto.Auth;
using RollerFate.Domain.Entities.Auth;

namespace RollerFate.Application.Auth;

public interface IAuthService
{
    Task<UserDto?> RegisterAsync(AuthData user);
    Task<TokenResponse?> LoginAsync(AuthData authData, HttpContext httpContext);
    Task<TokenResponse?> RefreshAsync(string refreshToken);
    Task<bool> RevokeRefreshAsync(string refreshToken);
}
