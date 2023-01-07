using System.Linq.Expressions;
using NotificationDotNet6SignalR.Domain.Abstracts;

namespace NotificationDotNet6SignalR.Domain.Repositories;

public interface IEntityRepository<TEntity> : IDisposable where TEntity : Entity, new()
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task Delete(TEntity entity);

    Task DeleteAll(IList<TEntity> entity);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
}