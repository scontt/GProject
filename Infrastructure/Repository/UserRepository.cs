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

    public IEnumerable<User> GetAll() => [.. _context.Users];
}
