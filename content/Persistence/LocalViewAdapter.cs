using System.Collections;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence;

internal class LocalViewAdapter<TEntity>(DbContext context) : ILocalView<TEntity>
    where TEntity : class
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