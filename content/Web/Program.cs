using Application;
using Persistence;
#if UseSerilogAspNetCore
using Serilog;
#endif
#if UseScalarAspNetCore
using Scalar.AspNetCore;
#endif

namespace Web;

file static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

#if UseSerilogAspNetCore
        await using var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        try
        {
            builder.Host.UseSerilog(logger);
#endif
            if (builder.Environment.IsDevelopment())
                builder.Host.UseDefaultServiceProvider(static serviceProviderOptions =>
                {
                    serviceProviderOptions.ValidateScopes = true;
                    serviceProviderOptions.ValidateOnBuild = true;
                });

            builder
                .AddPersistence()
                .AddApplication()
                .AddWeb();

            await using var app = builder.Build();

            if (app.Environment.IsProduction()) app.UseHsts();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
#if UseMiniProfilerAspNetCore
                app.UseMiniProfiler();
#endif

#if UseScalarAspNetCore
                app.MapScalarApiReference();
#endif
            }

            app.MapHealthChecks("/health");

            await app.RunAsync();
#if UseSerilogAspNetCore
        }
        catch (HostAbortedException)
        {
        }
        catch (Exception e)
        {
            logger.Fatal(e, "Application not started");
        }
#endif
    }
}