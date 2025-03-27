using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class HealthChecksBuilderExtensions
{
    public static IHealthChecksBuilder AddPersistenceCheck(this IHealthChecksBuilder builder)
    {
        return builder.AddDbContextCheck<AppDbContext>();
    }
}