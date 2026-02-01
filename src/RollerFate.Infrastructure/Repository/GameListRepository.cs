using Microsoft.EntityFrameworkCore;
using RollerFate.Application.Repository;
using RollerFate.Domain.Entities.Database;
using RollerFate.Infrastructure.Persistence.DataAccess;

namespace RollerFate.Infrastructure.Repository;

public class GameListRepository(ApplicationContext context) : IGameListRepository
{
    private readonly ApplicationContext _context = context;

    public async Task<GameList?> AddAsync(GameList entity)
    {
        if (entity is null)
            return null;

        await _context.GamesLists.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> AddGame(string listId, int gameId)
    {
        var list = await GetById(listId);
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (list is null || game is null)
            return false;

        list.Games ??= [];
        if (list.Games.Any(g => g.Id == gameId))
            return true;

        list.Games.Add(game);

        int rows = await _context.SaveChangesAsync();

        return rows > 0;
    }

    public async Task<bool> RemoveGame(string listId, int gameId)
    {
        var list = await GetById(listId);
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (list is null || game is null)
            return false;

        list.Games ??= [];
        list.Games.Remove(game);

        int rows = await _context.SaveChangesAsync();

        return rows > 0;
    }

    public async Task<IEnumerable<GameList>> GetAllAsync()
    {
        var lists = await _context.GamesLists.ToListAsync();

        return lists;
    }

    public async Task<GameList?> GetById(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return null;

        var gameList = await _context.GamesLists
            .Include(gl => gl.Games)
            .FirstOrDefaultAsync(gl => gl.Id == guidId);

        return gameList;
    }

    public async Task<GameList?> EditList(GameList updatedList)
    {
        var existing = await _context.GamesLists
            .Include(gl => gl.Games)
            .FirstOrDefaultAsync(gl => gl.Id == updatedList.Id);

        if (existing is null)
            return null;

        existing.Name = updatedList.Name;
        existing.Description = updatedList.Description;

        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<IEnumerable<GameList>?> GetByName(string name) =>
        await _context.GamesLists.Where(x => x.Name == name).ToListAsync();

    public async Task<ICollection<GameList>> GetByUserId(Guid userId)
    {
        return await _context.GamesLists
            .Include(gl => gl.Games)
            .Where(x => x.CreatorId == userId)
            .ToListAsync();
    }
}
