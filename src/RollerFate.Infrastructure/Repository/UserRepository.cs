using Microsoft.EntityFrameworkCore;
using RollerFate.Application.Abstractions.Repository;
using RollerFate.Domain.Entities.Database;
using RollerFate.Infrastructure.Persistence.DataAccess;

namespace RollerFate.Infrastructure.Repository;

public class UserRepository(ApplicationContext context) : IUserRepository
{
    private readonly ApplicationContext _context = context;

    public async Task<User?> AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        await _context.Entry(entity).ReloadAsync();
        return entity;
    }

    public async Task<User?> GetByUsername(string username) =>
        await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task<User?> GetById(string id) =>
        await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);

    public async Task<IEnumerable<User>?> GetByName(string name) =>
        await _context.Users.Where(x => x.Username == name).ToListAsync();
}
