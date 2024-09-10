using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Randevucum.Authentication.Orchestrator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.DefineMassTransit(configuration);
        //services.DefineGoogleOAuthSettings(configuration);
        return services;
    }

    public static void DefineMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumers(ApplicationAssembly.Assembly);
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMQ"), hst =>
                {
                    hst.Username("username");
                    hst.Password("password");
                });

                cfg.UseInMemoryOutbox(context);
                cfg.ConfigureEndpoints(context);
            });
        });
    }

    //private static void DefineGoogleOAuthSettings(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.Configure<GoogleOAuthSettings>(configuration.GetSection("GoogleOAuth"));
    //    services.AddTransient<IValidator<GoogleOAuthSettings>, GoogleOAuthSettingsValidator>();
    //}
}