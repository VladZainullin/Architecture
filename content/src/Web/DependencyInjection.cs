using Persistence;

namespace Web;

public static class DependencyInjection
{
    public static void AddWeb(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks().AddPersistenceCheck();
        builder.Services.AddOpenApi();
    }
}