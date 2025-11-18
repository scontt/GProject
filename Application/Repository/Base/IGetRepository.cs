namespace GProject.Application.Repository.Base;

public interface IGetRepository<T>
{
    T? GetById(int id);

    T? GetByName(string name);
}
