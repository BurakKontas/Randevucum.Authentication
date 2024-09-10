using Randevucum.Authentication.Orchestrator.Application.Services;
using Randevucum.Authentication.Orchestrator.Contracts.Requests;
using Randevucum.Authentication.Orchestrator.Contracts.Responses;

namespace Randevucum.Authentication.Orchestrator.API.Controllers.GraphQL;

[MutationType]
public class AuthenticationMutationController(IAuthenticationService authenticationService)
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [GraphQLName("GoogleLogin")]
    public async Task<AuthResponse> GoogleLogin(GoogleLoginRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.GoogleLogin(request, cancellationToken);
    }

    [GraphQLName("RefreshToken")]
    public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.RefreshToken(request, cancellationToken);
    }

    [GraphQLName("EmailLogin")]
    public async Task<object> EmailLogin(EmailLoginRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.EmailLogin(request, cancellationToken);
    }

    [GraphQLName("Logout")]
    public async Task<LogoutResponse> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.Logout(request, cancellationToken);
    }

    [GraphQLName("Register")]
    public async Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.Register(request, cancellationToken);
    }

    [GraphQLName("UpdateUser")]
    public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.UpdateUser(request, cancellationToken);
    }

    [GraphQLName("DeleteUser")]
    public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.DeleteUser(request, cancellationToken);
    }

    [GraphQLName("ChangePassword")]
    public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ChangePassword(request, cancellationToken);
    }

    [GraphQLName("ForgotPassword")]
    public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ForgotPassword(request, cancellationToken);
    }

    [GraphQLName("ResetPassword")]
    public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ResetPassword(request, cancellationToken);
    }

    [GraphQLName("SendVerificationEmail")]
    public async Task<SendEmailResponse> SendVerificationEmail(SendVerificationEmailRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.SendVerificationEmail(request, cancellationToken);
    }

    [GraphQLName("SendForgotPasswordEmail")]
    public async Task<SendEmailResponse> SendForgotPasswordEmail(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken)
    {
        return await _authenticationService.SendForgotPasswordEmail(request, cancellationToken);
    }
}