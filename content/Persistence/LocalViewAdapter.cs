using System.Collections;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence;

internal class LocalViewAdapter<TContext, TEntity>(TContext context) : ILocalView<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    public IEnumerator<TEntity> GetEnumerator()
    {
        return context.Set<TEntity>().Local.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}