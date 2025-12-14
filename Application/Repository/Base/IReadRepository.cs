namespace GProject.Application.Repository.Base;

public interface IReadRepository<T>
{
    T? GetById(string id);

    T? GetByName(string name);
}
