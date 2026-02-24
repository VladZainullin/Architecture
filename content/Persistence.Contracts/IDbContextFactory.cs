namespace Persistence.Contracts;

public interface IDbContextFactory
{
    Task<IDbContext> CreateAsync(CancellationToken cancellationToken = default);
    
    IDbContext Create();
}