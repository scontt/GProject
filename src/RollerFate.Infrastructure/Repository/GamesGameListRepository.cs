using RollerFate.Application.Repository;
using RollerFate.Domain.Entities.Database;
using RollerFate.Infrastructure.Persistence.DataAccess;

namespace RollerFate.Infrastructure.Repository;

public class GamesGameListRepository (ApplicationContext context) : IGamesGameListRepository
{
    private readonly ApplicationContext _context = context;

    public async Task<GamesGameList?> GetSingle(int gameId, Guid listId)
    {
        return await _context.Set<GamesGameList>()
            .FindAsync(gameId, listId);
    }

    public async Task<GamesGameList?> Update(GamesGameList listRel)
    {
        var existing = await _context.Set<GamesGameList>()
            .FindAsync(listRel.GameId, listRel.ListId);
        if (existing is null)
        {
            return null;
        }

        existing.User = listRel.User;
        await _context.SaveChangesAsync();
        return listRel;
    }
}
