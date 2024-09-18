using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Randevucum.Authentication.Microservices.Basic.Domain.Options;
using Randevucum.Authentication.Microservices.Basic.Domain.Providers;
using Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCockroachOptions(configuration);
        services.AddCockroachContexts();
        services.MigrateDatabase();
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

    private static void MigrateDatabase(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        try
        {
            var writeDbContext = serviceProvider.GetRequiredService<WriteDbContext>();
            //var readDbContext = serviceProvider.GetRequiredService<ReadDbContext>();

            //readDbContext.Database.Migrate(); // has no privilege to create database
            writeDbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            var assembly = InfrastructureAssembly.Assembly;
            var logger = serviceProvider.GetRequiredService<ILogger<object>>();
            logger.LogError(ex, $"An error occurred during migration in assembly {assembly.GetName().Name}.");
        }
    }
}