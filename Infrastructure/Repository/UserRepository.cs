using GProject.Application.Repository;
using GProject.DataAccess;
using GProject.Domain.Entities.Database;

namespace GProject.Infrastructure.Repository;

public class UserRepository(ApplicationContext context) : IUserRepository
{
    private readonly ApplicationContext _context = context;

    public User? Add(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();

        _context.Entry(entity).Reload();
        return entity;
    }

    public User? GetByUsername(string username) => _context.Users.FirstOrDefault(x => x.Username == username);

    public IEnumerable<User> GetAll() => [.. _context.Users];

    public User? GetById(string id) => _context.Users.FirstOrDefault(x => x.Id.ToString() == id);

    public User? GetByName(string name) => _context.Users.FirstOrDefault(x => x.Username == name);
}
