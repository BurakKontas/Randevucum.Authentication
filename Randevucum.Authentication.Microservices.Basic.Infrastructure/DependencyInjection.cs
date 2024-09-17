using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Randevucum.Authentication.Microservices.Basic.Domain.Options;
using Randevucum.Authentication.Microservices.Basic.Domain.Providers;
using Randevucum.Authentication.Microservices.Basic.Infrastructure.Contexts;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCockroachOptions(configuration);
        services.AddCockroachContexts();
        return services;
    }

    private static void AddCockroachOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CockroachOptions>("CockroachRead",
            options => { configuration.GetSection("CockroachDBRead").Bind(options); });

        services.Configure<CockroachOptions>("CockroachWrite",
            options => { configuration.GetSection("CockroachDBWrite").Bind(options); });

        services.AddTransient<CockroachOptionsProvider>();
    }

    private static void AddCockroachContexts(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var cockroachOptionsProvider = serviceProvider.GetRequiredService<CockroachOptionsProvider>();
        var migrationAssembly = typeof(WriteDbContext).Assembly.GetName().Name;

        services.AddDbContext<ReadDbContext>(options =>
        {
            options.UseNpgsql(cockroachOptionsProvider.Read.ConnectionString, b => b.MigrationsAssembly(migrationAssembly));
        });

        services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseNpgsql(cockroachOptionsProvider.Write.ConnectionString, b => b.MigrationsAssembly(migrationAssembly));
        });
    }
}