using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Services
{
    // A generic service that serves as the base implementation for specific entity services.
    public interface IGenericService<TEntity, TRequest, TResponse>
    where TEntity : class
    where TRequest : class
    where TResponse : class
    {
        Task<IEnumerable<TResponse>> GetAllAsync();

        Task<TResponse?> GetByIdAsync(int id);

        Task<TResponse> AddAsync(TRequest request);

        Task<TResponse> UpdateAsync(int id, TRequest request);

        Task DeleteAsync(int id);

        Task<IEnumerable<TResponse>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
