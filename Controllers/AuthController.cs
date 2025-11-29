using GProject.Application.Auth;
using GProject.Application.Repository;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Auth;
using GProject.Domain.Entities.Database;
using GProject.Infrastructure.Auth;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRepository userRepository, IAuthService authService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] AuthData authData)
    {
        if (authData is null)
            return BadRequest(ModelState);

        var newUser = await authService.RegisterAsync(authData);

        if (newUser == null)
            return BadRequest(ModelState);

        return Ok(newUser);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody]AuthData authData)
    {
        var searchedUser = userRepository.GetByUsername(authData.Username);

        if (searchedUser is null)
            return NotFound();

        string? token = await authService.LoginAsync(authData);

        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok(token);
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
