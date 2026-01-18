using GProject.Application.Repository;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IGameRepository gameRepository) : ControllerBase
{
    private readonly IGameRepository _gameRepository = gameRepository;

    [Authorize]
    [HttpGet("steam")]
    public async Task<ActionResult> GetAllFromSteam()
    {
        return Ok(await _gameRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var game = await _gameRepository.GetById(id);

        if (game == null)
            return NotFound();

        return Ok(game.Adapt<GameDto>());
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var game = (await _gameRepository.GetByName(name))?.ToList();

        if (game == null)
            return NotFound();

        return Ok(game.Adapt<List<GameDto>>());
    }
}
