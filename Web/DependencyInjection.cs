namespace Web;

public static class DependencyInjection
{
    public static void AddWeb(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        builder.Services.AddOpenApi();
    }
}