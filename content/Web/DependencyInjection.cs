using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
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
        builder.Services.Configure<ForwardedHeadersOptions>(static forwardedHeadersOptions =>
        {
            forwardedHeadersOptions.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto;
        });
        
        builder.Services.Configure<KestrelServerOptions>(static kestrelServerOptions =>
        {
            kestrelServerOptions.AddServerHeader = false;
        });
        
        builder.Services.AddResponseCompression(static options =>
        {
            options.EnableForHttps = true;
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes;

            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
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
        
#if UseScalarAspNetCore
        builder.Services.AddOpenApi();
#endif
    }
}