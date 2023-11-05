namespace API.Configurations;
public static partial class Extension
{
    public static IServiceCollection AddTalabeyahTicktingDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TalabeyahTicktingContext>((serviceProvider, option) =>
        {
            var configurations = serviceProvider.GetRequiredService<IConfiguration>();
            option.UseSqlServer(configurations.GetConnectionString(CommonNames.TalabeyahTicktingDatabase))
                  .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                   LogLevel.Information)
                  .EnableSensitiveDataLogging();
        });

        return services;
    }

    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var scopedServiceProvider = scoped.ServiceProvider;
        using var storeContext = scopedServiceProvider.GetRequiredService<TalabeyahTicktingContext>();
        storeContext.Database.Migrate();
        return app;
    }
}
