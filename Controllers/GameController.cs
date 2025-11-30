using GProject.Application.Repository;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController(IGameRepository gameRepository) : ControllerBase
{
    private readonly IGameRepository _gameRepository = gameRepository;

    [HttpGet]
    [Authorize]
    public ActionResult GetAll()
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

    [HttpPost]
    public ActionResult Add([FromBody] GameDto game)
    {
        if (game == null) return BadRequest(ModelState);

        var createGame = game.Adapt<Game>();
        _gameRepository.Add(createGame);

        return Ok();
    }
}
