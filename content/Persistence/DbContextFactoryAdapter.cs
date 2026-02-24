using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence;

internal sealed class DbContextFactoryAdapter<TContext>(IDbContextFactory<TContext> factory) : IDbContextFactory
    where TContext : DbContext
{
    public async Task<IDbContext> CreateAsync(CancellationToken cancellationToken = default)
    {
        var context = await factory.CreateDbContextAsync(cancellationToken);
        return new DbContextAdapter<TContext>(context);
    }

    public IDbContext Create()
    {
        var context = factory.CreateDbContext();
        return new DbContextAdapter<TContext>(context);
    }
}