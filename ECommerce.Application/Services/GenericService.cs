
using AutoMapper;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

//Generic Service which will work as the base for the other services
public class GenericService<TEntity, TRequest, TResponse>
        : IGenericService<TEntity, TRequest, TResponse>
        where TEntity : class
        where TRequest : class
        where TResponse : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TResponse>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<TResponse>>(entities);
    }

    public async Task<IEnumerable<TResponse>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _repository.FindAsync(predicate);
        return _mapper.Map<IEnumerable<TResponse>>(entities);
    }

    public async Task<TResponse?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return default;

        return _mapper.Map<TResponse>(entity);
    }

    public async Task<TResponse> AddAsync(TRequest request)
    {
        var entity = _mapper.Map<TEntity>(request);

        entity = await _repository.AddAsync(entity);

        return _mapper.Map<TResponse>(entity);
    }

    public async Task<TResponse> UpdateAsync(int id, TRequest request)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new KeyNotFoundException($"Record with Id {id} was not found.");

        _mapper.Map(request, entity);

        entity = await _repository.UpdateAsync(entity);

        return _mapper.Map<TResponse>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new KeyNotFoundException($"Record with Id {id} was not found.");

        await _repository.DeleteAsync(entity);
    }
}
