using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Persistence;

internal sealed class AppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var webDirectory = Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(webDirectory)
            .AddJsonFile("./Properties/launchSettings.json")
            .Build();

        var environmentVariablesSection = configuration.GetSection("profiles:https:environmentVariables");
        
        var connectionString = environmentVariablesSection
            .GetChildren()
            .Where(static section => section.Key.StartsWith("Postgres__"))
            .Select(static section => section.Key.Replace("Postgres__", string.Empty) + ':' + section.Value)
            .Aggregate(static (firstPart, secondPart) => firstPart + secondPart);

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        dbContextOptionsBuilder
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(connectionString, static builder =>
            {
                builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
            });
        
        return new AppDbContext(dbContextOptionsBuilder.Options);
    }
}