using GProject.Application.Repository.Base;
using GProject.Domain.Entities.Database;

namespace GProject.Application.Repository;

public interface IUserRepository : IRepository<User>, IReadRepository<User>
{
    Task<User?> GetByUsername(string username);
}
