using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Randevucum.Authentication.Microservices.Basic.Domain.Options;
using Randevucum.Authentication.Microservices.Basic.Domain.Providers;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCockroachOptions(configuration);
        return services;
    }

    public static void AddCockroachOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CockroachOptions>("CockroachRead", options =>
        {
            configuration.GetSection("CockroachDBRead").Bind(options);
        });

        services.Configure<CockroachOptions>("CockroachWrite", options =>
        {
            configuration.GetSection("CockroachDBWrite").Bind(options);
        });

        services.AddTransient<CockroachOptionsProvider>();
    }
}