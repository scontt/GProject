using RollerFate.Application.Abstractions.Repository.Base;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Application.Abstractions.Repository;

public interface IGameRepository : IRepository<Game>, IReadRepository<Game>
{
}
