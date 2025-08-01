using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence;

internal class DbSetAdapter<TEntity, TContext>(TContext context) : IDbSet<TEntity> 
    where TEntity : class
    where TContext : DbContext
{
    private LocalViewAdapter<TContext, TEntity>? _local;
    private readonly DbSet<TEntity> _set = context.Set<TEntity>();

    public virtual void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _set.UpdateRange(entities);
    }

    public virtual void Add(TEntity entity)
    {
        _set.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        _set.AddRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        _set.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _set.RemoveRange(entities);
    }

    public virtual void Attach(TEntity entity)
    {
        _set.Attach(entity);
    }

    public virtual void AttachRanga(IEnumerable<TEntity> entities)
    {
        _set.AttachRange(entities);
    }

    public ILocalView<TEntity> Local => _local ??= new LocalViewAdapter<TContext, TEntity>(context);

    public virtual IEnumerator<TEntity> GetEnumerator()
    {
        return ((IQueryable<TEntity>)_set).GetEnumerator();
    }

    public virtual Type ElementType => ((IQueryable<TEntity>)_set).ElementType;
    public virtual Expression Expression => ((IQueryable<TEntity>)_set).Expression;
    public virtual IQueryProvider Provider => ((IQueryable<TEntity>)_set).Provider;

    public virtual IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return ((IAsyncEnumerable<TEntity>)_set).GetAsyncEnumerator(cancellationToken);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}