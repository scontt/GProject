using GProject.Application.Repository;
using GProject.DataAccess;
using GProject.Domain.Entities.Database;

namespace GProject.Infrastructure.Repository;

public class GameRepository(ApplicationContext context) : IGameRepository
{
    private readonly ApplicationContext _context = context;

    public Game? Add(Game entity)
    {
        if (entity is not null)
        {
            _context.Games.Add(entity);

            _context.SaveChanges();
        }
        return entity;
    }

    public IEnumerable<Game> GetAll() => 
        [.. _context.Games];

    public Game? GetById(string id) => 
        _context.Games.FirstOrDefault(x => x.Id.ToString() == id);

    public IEnumerable<Game>? GetByName(string name) => 
        _context.Games.Where(g => g.Name.ToLower().Contains(name.ToLower()));
}
