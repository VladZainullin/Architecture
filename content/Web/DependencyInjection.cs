using Persistence;

namespace Web;

public static class DependencyInjection
{
    public static void AddWeb(this IHostApplicationBuilder builder)
    {
#if UseMiniProfilerAspNetCore
        builder.Services.AddMiniProfiler().AddEntityFramework();
#endif
        builder.Services.AddHealthChecks().AddPersistenceCheck();
        builder.Services.AddOpenApi();
    }
}