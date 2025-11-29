using GProject.Application.Repository;
using GProject.DataAccess;
using GProject.Domain.Entities.Database;

namespace GProject.Infrastructure.Repository;

public class GameListRepository(ApplicationContext context) : IGameListRepository
{
    private readonly ApplicationContext _context = context;

    public GamesList? Add(GamesList entity)
    {
        if (entity is not null)
            _context.GamesLists.Add(entity);

        return entity;
    }

    public IEnumerable<GamesList> GetAll() => [.. _context.GamesLists];

    public GamesList? GetById(string id) => _context.GamesLists.FirstOrDefault(x => x.Id.ToString() == id);

    public GamesList? GetByName(string name) => _context.GamesLists.FirstOrDefault(x => x.Name == name);
}
