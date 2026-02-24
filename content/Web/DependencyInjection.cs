using Microsoft.AspNetCore.Server.Kestrel.Core;
using Persistence;

namespace Web;

public static class DependencyInjection
{
    public static void AddWeb(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<KestrelServerOptions>(static kestrelServerOptions =>
        {
            kestrelServerOptions.AddServerHeader = false;
        });
        
#if UseMiniProfilerAspNetCore
        builder.Services.AddMiniProfiler().AddEntityFramework();
#endif
        builder.Services.AddHealthChecks().AddPersistenceCheck();
        builder.Services.AddOpenApi();
    }
}