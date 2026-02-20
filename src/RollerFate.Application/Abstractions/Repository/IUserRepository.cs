using RollerFate.Application.Abstractions.Repository.Base;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Application.Abstractions.Repository;

public interface IUserRepository : IRepository<User>, IReadRepository<User>
{
    Task<User?> GetByUsername(string username);
}
