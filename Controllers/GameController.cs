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
    public ActionResult GetAllFromSteam()
    {
        return Ok(_gameRepository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult GetById(string id)
    {
        var game = _gameRepository.GetById(id);

        if (game == null)
            return NotFound();

        return Ok(game.Adapt<GameDto>());
    }

    [HttpGet("name/{name}")]
    public ActionResult GetByName(string name)
    {
        var game = _gameRepository.GetByName(name)?.ToList();

        if (game == null)
            return NotFound();

        return Ok(game.Adapt<List<GameDto>>());
    }
}
