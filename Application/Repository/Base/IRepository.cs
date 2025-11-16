namespace GProject.Application.Repository.Base;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    T Add(T entity);
}
