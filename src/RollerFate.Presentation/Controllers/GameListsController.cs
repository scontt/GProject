using System.Security.Claims;
using RollerFate.Domain.Dto;
using RollerFate.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RollerFate.Application.Abstractions.Repository;

namespace RollerFate.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GameListsController(IGameListRepository gameListRepository, IGamesGameListRepository gamesGameListRepository, 
        IUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var list = await gameListRepository.GetAllAsync();

        if (!list.Any())
            return NotFound();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var list = await gameListRepository.GetById(id);

        if (list is null)
            return NotFound();

        return Ok(list.Adapt<GameListDto>());
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetByName(string name) 
    {
        var list = await gameListRepository.GetByName(name);

        if (list is null)
            return NotFound();

        return Ok(list.Adapt<GameListDto>());
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var list = await gameListRepository.GetByUserId(new Guid(userId));

        if (list.Count == 0)
            return NotFound();

        return Ok(list.Adapt<List<GameListDto>>());
    }

    [HttpPatch("addgame")]
    public async Task<IActionResult> AddGame([FromBody] GamePatch body)
    {
        if (string.IsNullOrEmpty(body.ListId) || string.IsNullOrEmpty(body.GameId))
        {
            return BadRequest(ModelState);
        }

        bool isAdded = await gameListRepository.AddGame(body.ListId, Convert.ToInt32(body.GameId));

        if (isAdded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> PatchList([FromBody] GameListDto listDto)
    {
        if (listDto.Id == Guid.Empty)
        {
            return BadRequest(ModelState);
        }

        var list = await gameListRepository.EditList(listDto.Adapt<GameList>());
        return Ok(list);
    }

    [HttpPatch("editgameuser")]
    public async Task<IActionResult> PatchGameUser([FromBody] GamesGameListDto listRelDto)
    {
        if (listRelDto.GameId == 0)
            return BadRequest(ModelState);

        var list = await gamesGameListRepository.GetSingle(listRelDto.GameId, listRelDto.ListId);

        if (list is null)
            return NotFound();

        list.User = await userRepository.GetById(listRelDto.UserId.ToString());

        var updated = await gamesGameListRepository.Update(list);

        return Ok(updated);
    }

    [HttpPatch("removegame")]
    public async Task<IActionResult> RemoveGame([FromBody] GamePatch body)
    {
        if (string.IsNullOrEmpty(body.ListId) || string.IsNullOrEmpty(body.GameId))
        {
            return BadRequest(ModelState);
        }

        bool isAdded = await gameListRepository.RemoveGame(body.ListId, Convert.ToInt32(body.GameId));

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
    public async Task<ActionResult> Create([FromBody] GameListDto list)
    {
        if (list.Id == Guid.Empty)
            return BadRequest(ModelState);

        var currentUserId = User.FindFirstValue("id");
        var adaptedList = list.Adapt<GameList>();
        adaptedList.CreatorId = new (currentUserId!);

        var newList = await gameListRepository.AddAsync(adaptedList);

        return Ok(newList.Adapt<GameListDto>());
    }
}
