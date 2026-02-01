using RollerFate.Application.Repository.Base;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Application.Repository;

public interface IGameListRepository : IRepository<GameList>, IReadRepository<GameList>
{
    Task<ICollection<GameList>> GetByUserId(Guid userId);
    Task<bool> AddGame(string listId, int gameId);
    Task<bool> RemoveGame(string listId, int gameId);
    Task<GameList?> EditList(GameList list);
}
