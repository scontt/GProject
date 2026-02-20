namespace RollerFate.Application.Abstractions.Repository.Base;

public interface IReadRepository<T>
{
    Task<T?> GetById(string id);

    Task<IEnumerable<T>?> GetByName(string name);
}
