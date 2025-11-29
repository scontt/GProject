using GProject.Domain.Dto;
using GProject.Domain.Entities.Auth;

namespace GProject.Application.Auth;

public interface IAuthService
{
    Task<UserDto?> RegisterAsync(AuthData user);
    Task<string?> LoginAsync(AuthData authData);
}
