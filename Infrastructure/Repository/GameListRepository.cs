using System.Data.Entity;
using GProject.Application.Repository;
using GProject.DataAccess;
using GProject.Domain.Entities.Database;
using Mapster;

namespace GProject.Infrastructure.Repository;

public class GameListRepository(ApplicationContext context) : IGameListRepository
{
    private readonly ApplicationContext _context = context;

    public GameList? Add(GameList entity)
    {
        if (entity is null)
            return null;

        _context.GamesLists.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public bool AddGame(string listId, int gameId)
    {
        var list = GetById(listId);
        var game = _context.Games.FirstOrDefault(g => g.Id == gameId);
        if (list is null || game is null)
            return false;

        list.Games ??= [];
        list.Games.Add(game);

        int rows = _context.SaveChanges();

        return rows > 0;
    }

    public bool RemoveGame(string listId, int gameId)
    {
        var list = GetById(listId);
        var game = _context.Games.FirstOrDefault(g => g.Id == gameId);
        if (list is null || game is null)
            return false;

        list.Games ??= [];
        list.Games.Remove(game);

        int rows = _context.SaveChanges();

        return rows > 0;
    }

    public IEnumerable<GameList> GetAll()
    {
        var lists = _context.GamesLists.ToList();

        return lists;
    }

    public GameList? GetById(string id)
    {
        var guidId = Guid.Parse(id);
        var gameList = _context.GamesLists.FirstOrDefault(gl => gl.Id == guidId);
        _context.Entry(gameList).Collection(gl => gl.Games).Load();

        return gameList;
    }

    public GameList? EditList(GameList updatedList)
    {
        var existing = _context.GamesLists
            .Include(gl => gl.Games)
            .FirstOrDefault(gl => gl.Id == updatedList.Id);

        if (existing is null)
            return null;

        existing.Name = updatedList.Name;
        existing.Description = updatedList.Description;

        _context.SaveChanges();

        return existing;
    }

    public IEnumerable<GameList>? GetByName(string name) => _context.GamesLists.Where(x => x.Name == name);

    public ICollection<GameList> GetByUserId(Guid userId)
    {
        var lists = _context.GamesLists.Where(x => x.CreatorId == userId).ToList();

        foreach (var item in lists)
        {
            _context.Entry(item).Collection(gl => gl.Games).Load();
        }

        return lists;
    }
}
