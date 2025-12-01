using GProject.Domain.Dto;
using GProject.Domain.Dto.Auth;
using GProject.Domain.Entities.Auth;

namespace GProject.Application.Auth;

public interface IAuthService
{
    Task<UserDto?> RegisterAsync(AuthData user);
    Task<TokenResponse?> LoginAsync(AuthData authData, HttpContext httpContext);
    Task<TokenResponse?> RefreshAsync(string refreshToken);
}
