using Grpc.Core;
using Randevucum.Authentication.Orchestrator.API.Controllers.Adapters;
using Randevucum.Authentication.Orchestrator.Application.Services;
using Randevucum.Authentication.Orchestrator.Domain;

namespace Randevucum.Authentication.Orchestrator.API.Controllers.Grpc;

public class AuthenticationGrpcController(ILogger<AuthenticationGrpcController> logger, IAuthenticationService authenticationService) : AuthService.AuthServiceBase
{
    private readonly ILogger<AuthenticationGrpcController> _logger = logger;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public override async Task<AuthResponse> GoogleLogin(GoogleLoginRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.GoogleLogin(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<AuthResponse> RefreshToken(RefreshTokenRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.RefreshToken(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<AuthResponse> EmailLogin(EmailLoginRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.EmailLogin(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.Logout(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.Register(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.UpdateUser(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.DeleteUser(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request,
        ServerCallContext context)
    {
        var result = await _authenticationService.ChangePassword(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request,
        ServerCallContext context)
    {
        var result = await _authenticationService.ForgotPassword(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.ResetPassword(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<SendEmailResponse> SendVerificationEmail(SendVerificationEmailRequest request,
        ServerCallContext context)
    {
        var result = await _authenticationService.SendVerificationEmail(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<SendEmailResponse> SendForgotPasswordEmail(SendForgotPasswordEmailRequest request,
        ServerCallContext context)
    {
        var result = await _authenticationService.SendForgotPasswordEmail(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.GetUser(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<GetUserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.GetUserById(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }

    public override async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, ServerCallContext context)
    {
        var result = await _authenticationService.ValidateToken(request.ToContract(), context.CancellationToken);
        return result.ToGrpc();
    }
}