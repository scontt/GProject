using GProject.Application.Repository;
using GProject.Domain.Entities.Auth;
using GProject.Domain.Entities.Database;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("signup")]
    public ActionResult SignUp([FromBody] User user)
    {
        var createdUser = _userRepository.Add(user);

        if (createdUser is null)
            return BadRequest(user);

        return Ok(user);
    }

    [HttpPost("signin")]
    public ActionResult SignIn([FromBody]AuthData credentials)
    {
        var searchedUser = _userRepository.GetByUsername(credentials.Username);
        if (searchedUser is null)
            return NotFound();

        if (credentials.Password != searchedUser.Password)
            return Unauthorized();

        return Ok(searchedUser);
    }

    [HttpGet("all")]
    public ActionResult<List<User>> GetAll()
    {
        var users = _userRepository.GetAll();

        if (users is null)
            return NotFound();

        return Ok(users);
    }
}
