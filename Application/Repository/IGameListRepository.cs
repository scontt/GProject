using GProject.Application.Repository.Base;
using GProject.Domain.Entities.Database;

namespace GProject.Application.Repository;

public interface IGameListRepository : IRepository<GamesList>, IGetRepository<GamesList>
{
}
