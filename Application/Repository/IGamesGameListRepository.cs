using GProject.Application.Repository.Base;
using GProject.Domain.Entities.Database;

namespace GProject.Application.Repository;

public interface IGamesGameListRepository
{
    Task<GamesGameList?> GetSingle(int gameId, Guid listId);
    Task<GamesGameList?> Update(GamesGameList listRel);
}