using Randevucum.Authentication.Orchestrator.Application.Services;
using Randevucum.Authentication.Orchestrator.Contracts.Requests;
using Randevucum.Authentication.Orchestrator.Contracts.Responses;

namespace Randevucum.Authentication.Orchestrator.API.Controllers.GraphQL;

[QueryType]
public class AuthenticationQueryController(IAuthenticationService authenticationService)
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [GraphQLName("GenerateGoogleLoginUrl")]
    public async Task<string> GenerateGoogleLoginUrl(CancellationToken cancellationToken)
    {
        return await _authenticationService.GenerateGoogleLoginUrl(cancellationToken);
    }

    [GraphQLName("GetUser")]
    public async Task<GetUserResponse> GetUser(GetUserRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.GetUser(request, cancellationToken);
    }

    [GraphQLName("GetUserById")]
    public async Task<GetUserResponse> GetUserById(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.GetUserById(request, cancellationToken);
    }

    [GraphQLName("ValidateToken")]
    public async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ValidateToken(request, cancellationToken);
    }
}