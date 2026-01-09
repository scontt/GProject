using GProject.Application.Repository.Base;
using GProject.Domain.Entities.Database;

namespace GProject.Application.Repository;

public interface IGameListRepository : IRepository<GameList>, IReadRepository<GameList>
{
    IEnumerable<GameList> GetByUserId(Guid userId);
    bool AddGame(string listId, int gameId);
    bool RemoveGame(string listId, int gameId);
}
