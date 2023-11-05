using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Configurations;
public static class Registration
{
    public static async Task RunTicketsAsync(this WebApplicationBuilder builder)
    {
        var logger = default(Logger);
        var app = default(WebApplication);
        try
        {
            logger = Extension.GetLogger();
            app = builder.Build();
            logger.Info($"Service Talabeyah-TicktingSystem Starts Successfully");
            await app.AddMiddlewares()
                .ApplyMigrations()
                .RunAsync();
        }
        catch (Exception exception)
        {
            logger?.Error(exception, $"Service Talabeyah-TicktingSystem Stopping Due To Exception");
            await app?.StopAsync()!;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
    public static IServiceCollection AddTicketsServices(this IServiceCollection services)
    {
        services
           .AddScoped<ITicketRepository, TicketRepository>()
           .AddControllerConfigurations();
        return services;
    }

    public static IServiceCollection ConfigureBadRequestBehaviour(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                                        .Where(error => error.Value?.Errors?.Any() ?? false)
                                        .SelectMany(x => x.Value?.Errors ?? new List<ModelError>(0) as ICollection<ModelError>)
                                        .Select(x => x.ErrorMessage)
                                        .ToList();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };

        });

        return services;
    }

    private static IServiceCollection AddControllerConfigurations(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();
        return services;
    }
}
