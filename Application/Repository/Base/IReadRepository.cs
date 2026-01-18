namespace GProject.Application.Repository.Base;

public interface IReadRepository<T>
{
    Task<T?> GetById(string id);

    Task<IEnumerable<T>?> GetByName(string name);
}
