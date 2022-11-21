using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Domain.Abstracts;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Infra.Repositories;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity, new()

{
	protected readonly NotificationDotNet6SignalRDataContext _context;
	protected DbSet<TEntity> _dbSet;

    public EntityRepository(NotificationDotNet6SignalRDataContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(string.Concat("UPDATE: ", this.GetType().Name), ex);
        }

        return entity;
    }

    public virtual async Task Delete(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(string.Concat("DELETE: ", this.GetType().Name), ex);
        }
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
        => await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id) ?? Activator.CreateInstance<TEntity>();

    public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync() ?? Activator.CreateInstance<TEntity>();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}