using MassTransit;
using Randevucum.Authentication.Common.OAuth.GoogleLogin;
using Randevucum.Authentication.Orchestrator.Contracts.Common;
using Randevucum.Authentication.Orchestrator.Contracts.Requests;
using Randevucum.Authentication.Orchestrator.Contracts.Responses;

namespace Randevucum.Authentication.Orchestrator.Application.Services;

public class AuthenticationService(IBus bus) : IAuthenticationService
{
    private readonly IBus _bus = bus;

    public async Task<AuthResponse> GoogleLogin(GoogleLoginRequest request, CancellationToken cancellationToken)
    {
        var requestEvent = new GoogleLoginReceivedEvent(request.AuthenticationCode);

        var response = await _bus.Request<GoogleLoginReceivedEvent, GoogleLoginResultedEvent>(requestEvent, cancellationToken: cancellationToken);

        var authResponse = new AuthResponse(
            new OperationResult(response.Message.IsSuccess, response.Message.Message, response.Message.StatusCode),
            response.Message.Token,
            response.Message.RefreshToken
        );

        return authResponse;
    }

    public async Task<AuthResponse> EmailLogin(EmailLoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<GetUserResponse> GetUser(GetUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<GetUserResponse> GetUserById(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<LogoutResponse> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<SendEmailResponse> SendVerificationEmail(SendVerificationEmailRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<SendEmailResponse> SendForgotPasswordEmail(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<RoleCheckResponse> RoleCheck(RoleCheckRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<VerifyEmailResponse> VerifyEmailWithOneTimeCode(VerifyEmailWithOneTimeCodeRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<VerifyEmailResponse> VerifyEmailWithCode(VerifyEmailWithCodeRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateGoogleLoginUrl(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}