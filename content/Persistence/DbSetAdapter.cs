using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence;

internal class DbSetAdapter<T>(DbContext context) : IDbSet<T> where T : class
{
    private readonly DbSet<T> _set = context.Set<T>();

    public virtual void Update(T entity)
    {
        _set.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _set.UpdateRange(entities);
    }

    public virtual void Add(T entity)
    {
        _set.Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _set.AddRange(entities);
    }

    public virtual void Remove(T entity)
    {
        _set.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _set.RemoveRange(entities);
    }

    public virtual void Attach(T entity)
    {
        _set.Attach(entity);
    }

    public virtual void AttachRanga(IEnumerable<T> entities)
    {
        _set.AttachRange(entities);
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
        return ((IQueryable<T>)_set).GetEnumerator();
    }

    public virtual Type ElementType => ((IQueryable<T>)_set).ElementType;
    public virtual Expression Expression => ((IQueryable<T>)_set).Expression;
    public virtual IQueryProvider Provider => ((IQueryable<T>)_set).Provider;

    public virtual IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return ((IAsyncEnumerable<T>)_set).GetAsyncEnumerator(cancellationToken);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}