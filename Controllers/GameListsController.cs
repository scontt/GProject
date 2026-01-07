using System.Security.Claims;
using GProject.Application.Repository;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Route("[controller]")]
public class GameListsController(IGameListRepository gameListRepository) : ControllerBase
{
    private readonly IGameListRepository _gameListRepository = gameListRepository;

    [HttpGet]
    public ActionResult GetAll()
    {
        var list = _gameListRepository.GetAll();

        if (list is null)
            return NotFound();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public ActionResult GetById(string id)
    {
        var list = _gameListRepository.GetById(id);

        if (list is null)
            return NotFound();

        return Ok(list);
    }

    [HttpGet("name/{name}")]
    public ActionResult GetByName(string name) 
    {
        var list = _gameListRepository.GetByName(name);

        if (list is null)
            return NotFound();

        return Ok(list.Adapt<GameListDto>());
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(string userId)
    {
        var list = _gameListRepository.GetByUserId(userId);

        if (list is null || list.Count() == 0)
            return NotFound();

        return Ok(list.Adapt<GameListDto>());
    }


    [HttpPost]
    [Authorize]
    public ActionResult Create([FromBody] GameListDto list)
    {
        if (list is null)
            return BadRequest(ModelState);

        var currentUserId = User.FindFirstValue("id");
        list.Id = new(currentUserId!);
        var newList = _gameListRepository.Add(list.Adapt<GameList>());

        return Ok(newList);
    }
}
