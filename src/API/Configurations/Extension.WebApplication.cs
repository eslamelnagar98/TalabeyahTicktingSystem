namespace API.Configurations;
public partial class Extension
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExeptionMiddleware>();
        app.UseCors("CorsPolicy");
        if (app.Environment.IsDevelopment())
        {
            app
           .UseSwagger()
           .UseSwaggerUI();
        }
        app.MapControllers();
        return app;
    }

    public static Logger GetLogger()
    {
        return LogManager.Setup()
            .LoadConfigurationFromAppSettings()
            .GetCurrentClassLogger();
    }
}
