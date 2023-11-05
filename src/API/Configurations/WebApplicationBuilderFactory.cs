namespace API.Configurations;
public class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder Create(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            .AddTalabeyahTicktingDbContext()
            .AddTicketsServices()
            .ConfigureBadRequestBehaviour();
        return builder;
    }
}
