using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Randevucum.Authentication.Microservices.Basic.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly.Assembly));
            return services;
        }
    }
}