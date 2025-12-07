using GProject.Application.Repository;
using GProject.Domain.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.FindFirst("id")?.Value;
        if (userId == null)
            return Unauthorized();

        var user = userRepository.GetById(userId);

        return Ok(user.Adapt<UserDto>());
    }
}
