using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;
using Persistence.Contracts;

namespace Persistence;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOptions<NpgsqlConnectionStringBuilder>().BindConfiguration("Postgres");
        builder.Services.AddDbContextPool<AppDbContext>(static (sp, options) =>
        {
            var npgsqlConnectionStringBuilderOptions = sp.GetRequiredService<IOptions<NpgsqlConnectionStringBuilder>>();
            var npgsqlConnectionStringBuilder = npgsqlConnectionStringBuilderOptions.Value;
            var connectionString = npgsqlConnectionStringBuilder.ConnectionString;
            options
                .UseSnakeCaseNamingConvention()
                .UseNpgsql(connectionString, npgsqlDbContextOptionsBuilder =>
                {
                    npgsqlDbContextOptionsBuilder.MigrationsAssembly("Persistence");
                    npgsqlDbContextOptionsBuilder.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        npgsqlConnectionStringBuilder.SearchPath);
                });
        });

        builder.Services.AddScoped<DbContext>(static sp => sp.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<DbContextAdapter>();

        var getDbContextAdapter = 
            static (IServiceProvider sp) => sp.GetRequiredService<DbContextAdapter>();
        
        builder.Services.AddScoped<IDbContext>(getDbContextAdapter);
        builder.Services.AddScoped<IMigrationContext>(getDbContextAdapter);
        builder.Services.AddScoped<ITransactionContext>(getDbContextAdapter);

        return builder;
    }
}