using GProject.Application.Auth;
using GProject.Application.Repository;
using GProject.Domain.Entities.Auth;
using GProject.Domain.Entities.Database;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> SignIn([FromBody]AuthData authData)
    {
        var searchedUser = userRepository.GetByUsername(authData.Username);

        if (searchedUser is null)
            return Unauthorized();

        string? token = await authService.LoginAsync(authData);

        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok($"access: {token}");
    }

    [HttpGet("all")]
    public ActionResult<List<User>> GetAll()
    {
        var users = userRepository.GetAll();

        if (users is null)
            return NotFound();

        return Ok(users);
    }
}
