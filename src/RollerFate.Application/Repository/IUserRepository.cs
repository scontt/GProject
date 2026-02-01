using RollerFate.Application.Repository.Base;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Application.Repository;

public interface IUserRepository : IRepository<User>, IReadRepository<User>
{
    Task<User?> GetByUsername(string username);
}
