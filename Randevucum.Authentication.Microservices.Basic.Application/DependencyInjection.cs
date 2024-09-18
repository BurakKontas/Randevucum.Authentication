using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Randevucum.Authentication.Microservices.Basic.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.DefineMassTransit(configuration);
        return services;
    }

    private static void DefineMassTransit(this IServiceCollection services, IConfiguration configuration)
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
}