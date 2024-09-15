using System.Reflection;
using System.Text.Json;
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
        var readOptionsValue = configuration.GetSection("CockroachDBRead").Value;
        var writeOptionsValue = configuration.GetSection("CockroachDBWrite").Value;

        if (readOptionsValue is null) throw new ArgumentNullException($"CockroachDBRead options are not found in the configuration.");
        if (writeOptionsValue is null) throw new ArgumentNullException($"CockroachDBWrite options are not found in the configuration.");
        
        var readOptions = JsonSerializer.Deserialize<CockroachOptions>(readOptionsValue);
        var writeOptions = JsonSerializer.Deserialize<CockroachOptions>(writeOptionsValue);

        CockroachOptionsProvider.Configure(readOptions!, writeOptions!);
    }


}