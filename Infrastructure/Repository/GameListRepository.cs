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

        _context.GamesLists.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public IEnumerable<GameList> GetAll() => [.. _context.GamesLists];

    public GameList? GetById(string id) => _context.GamesLists.FirstOrDefault(x => x.Id.ToString() == id);

    public IEnumerable<GameList>? GetByName(string name) => _context.GamesLists.Where(x => x.Name == name);

    public IEnumerable<GameList> GetByUserId(string userId) => _context.GamesLists.Where(x => x.CreatorId.ToString() == userId);
}
