namespace GProject.Application.Repository.Base;

public interface IGetRepository<T>
{
    T? GetById(string id);

    T? GetByName(string name);
}
