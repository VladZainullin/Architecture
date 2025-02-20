using Application;
using Persistence;
using Scalar.AspNetCore;
using Serilog;

namespace Web;

file static class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        await using var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        
        var builder = WebApplication.CreateBuilder(args);

        builder
            .AddPersistence()
            .AddApplication()
            .AddWeb();
        
        await using var app = builder.Build();

        if (app.Environment.IsProduction())
        {
            app.UseHsts();
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
        app.MapHealthChecks("/health");

        await app.RunAsync();
    }
}