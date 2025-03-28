using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence;

internal sealed class DbContextAdapter(AppDbContext context) :
    IDbContext,
    IMigrationContext,
    ITransactionContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.MigrateAsync(cancellationToken);
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.BeginTransactionAsync(cancellationToken);
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.CommitTransactionAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.RollbackTransactionAsync(cancellationToken);
    }
}