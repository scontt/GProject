using RollerFate.Application.Repository.Base;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Application.Repository;

public interface IGameRepository : IRepository<Game>, IReadRepository<Game>
{
}
