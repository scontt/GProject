using RollerFate.Domain.Entities.Database;

namespace RollerFate.Application.Abstractions.Repository;

public interface IGamesGameListRepository
{
    Task<GamesGameList?> GetSingle(int gameId, Guid listId);
    Task<GamesGameList?> Update(GamesGameList listRel);
}