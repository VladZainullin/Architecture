using Application;
using Persistence;
using Scalar.AspNetCore;
using Serilog;

namespace Web;

file static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        await using var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        try
        {
            builder.Host.UseSerilog(logger);
            
            if (builder.Environment.IsDevelopment())
            {
                builder.Host.UseDefaultServiceProvider(static serviceProviderOptions =>
                {
                    serviceProviderOptions.ValidateScopes = true;
                    serviceProviderOptions.ValidateOnBuild = true;
                });
            }

            builder
                .AddPersistence()
                .AddApplication()
                .AddWeb();

            await using var app = builder.Build();

            if (app.Environment.IsProduction()) app.UseHsts();
            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.MapHealthChecks("/health");

            await app.RunAsync();
        }
        catch (HostAbortedException)
        {
        }
        catch (Exception e)
        {
            logger.Fatal(e, "Application not started");
        }
    }
}