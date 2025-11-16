using GProject.Application.Repository;
using GProject.DataAccess;
using GProject.Domain.Entities;

namespace GProject.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;
    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public User Add(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();

        _context.Entry(entity).Reload();
        return entity;
    }

    public IEnumerable<User> GetAll() => [.. _context.Users];
}
