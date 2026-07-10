using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity);

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    Task<T> UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}