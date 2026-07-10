using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.Repositories;

//Generic Repository which will work as the base for the other services
public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    //included the no tracking to avoid which is optimization
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    //included the no tracking to avoid which is optimization
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return entities;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
}