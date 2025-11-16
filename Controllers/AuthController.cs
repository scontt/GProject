using GProject.Application.Repository;
using GProject.Domain.Entities;
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

    [HttpPost]
    public IActionResult SignUp()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
        var users = _userRepository.GetAll();

        if (users is null)
            return NotFound();

        return Ok(users);
    }
}
