using GProject.Application.Repository;
using GProject.Domain.Dto;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesListController(IGameListRepository gameListRepository) : ControllerBase
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

        return Ok(list.Adapt<GamesListDto>());
    }

    [HttpPost]
    public ActionResult Add([FromBody] GamesListDto list)
    {
        if (list is null)
            return BadRequest(ModelState);

        var newList = _gameListRepository.Add(list.Adapt<GamesList>());

        return Ok(newList);
    }
}
