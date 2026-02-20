namespace RollerFate.Application.Abstractions.Repository.Base;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> AddAsync(T entity);
}
