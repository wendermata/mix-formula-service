using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
