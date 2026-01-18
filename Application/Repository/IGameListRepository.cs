using GProject.Application.Repository.Base;
using GProject.Domain.Entities.Database;

namespace GProject.Application.Repository;

public interface IGameListRepository : IRepository<GameList>, IReadRepository<GameList>
{
    Task<ICollection<GameList>> GetByUserId(Guid userId);
    Task<bool> AddGame(string listId, int gameId);
    Task<bool> RemoveGame(string listId, int gameId);
    Task<GameList?> EditList(GameList list);
}
