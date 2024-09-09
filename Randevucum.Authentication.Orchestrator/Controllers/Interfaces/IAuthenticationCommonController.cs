using Randevucum.Authentication.Orchestrator.Contracts.Requests;
using Randevucum.Authentication.Orchestrator.Contracts.Responses;

namespace Randevucum.Authentication.Orchestrator.API.Controllers.Interfaces;

public interface IAuthenticationCommonController
{
    Task<AuthResponse> GoogleLogin(GoogleLoginRequest request, CancellationToken cancellationToken);
    Task<AuthResponse> EmailLogin(EmailLoginRequest request, CancellationToken cancellationToken);
    Task<AuthResponse> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<GetUserResponse> GetUser(GetUserRequest request, CancellationToken cancellationToken);
    Task<GetUserResponse> GetUserById(GetUserByIdRequest request, CancellationToken cancellationToken);
    Task<LogoutResponse> Logout(LogoutRequest request, CancellationToken cancellationToken);
    Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken);
    Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken);
    Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, CancellationToken cancellationToken);
    Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, CancellationToken cancellationToken);
    Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken);
    Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken);
    Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken);
    Task<SendEmailResponse> SendVerificationEmail(SendVerificationEmailRequest request, CancellationToken cancellationToken);
    Task<SendEmailResponse> SendForgotPasswordEmail(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken);
    Task<RoleCheckResponse> RoleCheck(RoleCheckRequest request, CancellationToken cancellationToken);
    Task<VerifyEmailResponse> VerifyEmailWithOneTimeCode(VerifyEmailWithOneTimeCodeRequest request, CancellationToken cancellationToken);
    Task<VerifyEmailResponse> VerifyEmailWithCode(VerifyEmailWithCodeRequest request, CancellationToken cancellationToken);
    Task<string> GenerateGoogleLoginUrl(CancellationToken cancellationToken);
}