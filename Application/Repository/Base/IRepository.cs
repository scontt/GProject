namespace GProject.Application.Repository.Base;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> AddAsync(T entity);
}
