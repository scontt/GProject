using GProject.Application.Repository.Base;
using GProject.Domain.Entities.Database;

namespace GProject.Application.Repository;

public interface IUserRepository : IRepository<User>, IGetRepository<User>
{
    User? GetByUsername(string username);
}
