using GProject.Application.Auth;
using GProject.Application.Repository;
using GProject.Domain.Dto.Auth;
using GProject.Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRepository userRepository, IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> SignUp([FromBody] AuthData authData)
    {
        if (authData is null)
            return BadRequest(ModelState);

        if (string.IsNullOrEmpty(authData.Username) || string.IsNullOrEmpty(authData.Password))
            return BadRequest(ModelState);

        var newUser = await authService.RegisterAsync(authData);

        if (newUser == null)
            return BadRequest(ModelState);

        return Ok(newUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignIn([FromBody] AuthData authData)
    {
        var searchedUser = userRepository.GetByUsername(authData.Username);

        if (searchedUser is null)
            return Unauthorized();

        TokenResponse? tokens = await authService.LoginAsync(authData, HttpContext);

        if (tokens is null)
            return Unauthorized();

        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            Path = "/",
        };

        Response.Cookies.Append("ahaha", tokens.AccessToken, cookieOptions);

        return Ok(new { accessToken = tokens.AccessToken });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("ahaha");

        return Ok();
    }


    [Authorize]
    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        var userId = User.FindFirstValue("id");
        return Ok(userId);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        if (request is null || string.IsNullOrEmpty(request.RefreshToken))
            return BadRequest();

        var tokens = await authService.RefreshAsync(request.RefreshToken);

        if (tokens is null)
            return Unauthorized();

        return Ok(tokens);
    }
}
