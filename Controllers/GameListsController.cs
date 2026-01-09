using System.Security.Claims;
using GProject.Application.Repository;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GameListsController(IGameListRepository gameListRepository, IGameRepository gameRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult GetAll()
    {
        var list = gameListRepository.GetAll();

        if (list is null)
            return NotFound();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public ActionResult GetById(string id)
    {
        var list = gameListRepository.GetById(id);

        if (list is null)
            return NotFound();

        return Ok(list.Adapt<GameListDto>());
    }

    [HttpGet("name/{name}")]
    public ActionResult GetByName(string name) 
    {
        var list = gameListRepository.GetByName(name);

        if (list is null)
            return NotFound();

        return Ok(list.Adapt<GameListDto>());
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(string userId)
    {
        var list = gameListRepository.GetByUserId(new(userId));

        if (list is null)
            return NotFound();

        return Ok(list.Adapt<List<GameListDto>>());
    }

    [HttpPatch("addgame")]
    public IActionResult AddGame([FromBody] GamePatch body)
    {
        if (string.IsNullOrEmpty(body.ListId) || string.IsNullOrEmpty(body.GameId))
        {
            return BadRequest(ModelState);
        }

        bool isAdded = gameListRepository.AddGame(body.ListId, Convert.ToInt32(body.GameId));

        if (isAdded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("removegame")]
    public IActionResult RemoveGame([FromBody] GamePatch body)
    {
        if (string.IsNullOrEmpty(body.ListId) || string.IsNullOrEmpty(body.GameId))
        {
            return BadRequest(ModelState);
        }

        bool isAdded = gameListRepository.RemoveGame(body.ListId, Convert.ToInt32(body.GameId));

        if (isAdded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPost]
    public ActionResult Create([FromBody] GameListDto list)
    {
        if (list is null)
            return BadRequest(ModelState);

        var currentUserId = User.FindFirstValue("id");
        var adaptedList = list.Adapt<GameList>();
        adaptedList.CreatorId = new (currentUserId!);

        var newList = gameListRepository.Add(adaptedList);

        return Ok(newList.Adapt<GameListDto>());
    }
}
