using Microsoft.Extensions.Options;
using Randevucum.Authentication.Orchestrator.API.Services.Interfaces;
using Randevucum.Authentication.Orchestrator.Contracts.Requests;
using Randevucum.Authentication.Orchestrator.Contracts.Responses;

namespace Randevucum.Authentication.Orchestrator.API.Services.GraphQL;

[QueryType]
public class AuthenticationQueryService(IAuthenticationCommonService authenticationService)
{
    [GraphQLName("GenerateGoogleLoginUrl")]
    public async Task<string> GenerateGoogleLoginUrl(CancellationToken cancellationToken)
    {
        return await authenticationService.GenerateGoogleLoginUrl(cancellationToken);
    }

    [GraphQLName("GetUser")]
    public async Task<GetUserResponse> GetUser(GetUserRequest request, CancellationToken cancellationToken)
    {
        return await authenticationService.GetUser(request, cancellationToken);
    }

    [GraphQLName("ValidateToken")]
    public async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, CancellationToken cancellationToken)
    {
        return await authenticationService.ValidateToken(request, cancellationToken);
    }
}