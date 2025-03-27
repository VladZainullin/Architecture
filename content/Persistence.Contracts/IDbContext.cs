namespace Persistence.Contracts;

public interface IDbContext
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}