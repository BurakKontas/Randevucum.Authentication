// ReSharper disable InconsistentNaming
using HotChocolate.Types.Pagination;
using Randevucum.Authentication.Orchestrator.API.Controllers.GraphQL;

namespace Randevucum.Authentication.Orchestrator.API.Extensions;

public static class DefineGraphQLExtension
{
    public static IServiceCollection DefineGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType()
            .AddTypeExtension<AuthenticationQueryController>()
            .AddMutationType()
            .AddTypeExtension<AuthenticationMutationController>()
            .SetPagingOptions(new PagingOptions { DefaultPageSize = 20 });
        return services;
    }
}