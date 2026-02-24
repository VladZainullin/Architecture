using Microsoft.AspNetCore.Server.Kestrel.Core;
using Persistence;
#if UseSerilogAspNetCore
using Serilog;
#endif

namespace Web;

public static class DependencyInjection
{
    public static void AddWeb(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<KestrelServerOptions>(static kestrelServerOptions =>
        {
            kestrelServerOptions.AddServerHeader = false;
        });

#if UseSerilogAspNetCore
        builder.Services.AddSerilog();
#endif
        if (builder.Environment.IsDevelopment())
            builder.Host.UseDefaultServiceProvider(static serviceProviderOptions =>
            {
                serviceProviderOptions.ValidateScopes = true;
                serviceProviderOptions.ValidateOnBuild = true;
            });

#if UseMiniProfilerAspNetCore
        builder.Services.AddMiniProfiler().AddEntityFramework();
#endif
        builder.Services.AddHealthChecks().AddPersistenceCheck();
        builder.Services.AddOpenApi();
    }
}