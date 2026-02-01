using Microsoft.EntityFrameworkCore;
using RollerFate.Application.Repository;
using RollerFate.Domain.Entities.Database;
using RollerFate.Infrastructure.Persistence.DataAccess;

namespace RollerFate.Infrastructure.Repository;

public class GameRepository(ApplicationContext context) : IGameRepository
{
    public async Task<Game?> AddAsync(Game entity)
    {
        if (entity is not null)
        {
            await context.Games.AddAsync(entity);

            await context.SaveChangesAsync();
        }
        return entity;
    }

    public async Task<IEnumerable<Game>> GetAllAsync() =>
        await context.Games.ToListAsync();

    public async Task<Game?> GetById(string id) =>
        await context.Games.FirstOrDefaultAsync(x => x.Id.ToString() == id);

    public async Task<IEnumerable<Game>?> GetByName(string name) =>
        await context.Games.Where(g => g.Name.ToLower().Contains(name.ToLower())).ToListAsync();
}
