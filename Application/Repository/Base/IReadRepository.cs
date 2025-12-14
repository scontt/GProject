namespace GProject.Application.Repository.Base;

public interface IReadRepository<T>
{
    T? GetById(string id);

    IEnumerable<T>? GetByName(string name);
}
