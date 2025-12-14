using GProject.Application.Repository;
using GProject.DataAccess;
using GProject.Domain.Entities.Database;

namespace GProject.Infrastructure.Repository;

public class GameListRepository(ApplicationContext context) : IGameListRepository
{
    private readonly ApplicationContext _context = context;

    public GameList? Add(GameList entity)
    {
        if (entity is null)
            return null;

        entity.User = _context.Users.First(x => x.Id == entity.CreatorId);
        _context.GamesLists.Add(entity);

        return entity;
    }

    public IEnumerable<GameList> GetAll() => [.. _context.GamesLists];

    public GameList? GetById(string id) => _context.GamesLists.FirstOrDefault(x => x.Id.ToString() == id);

    public GameList? GetByName(string name) => _context.GamesLists.FirstOrDefault(x => x.Name == name);

    public IEnumerable<GameList> GetByUserId(string userId) => _context.GamesLists.Where(x => x.CreatorId.ToString() == userId);
}
