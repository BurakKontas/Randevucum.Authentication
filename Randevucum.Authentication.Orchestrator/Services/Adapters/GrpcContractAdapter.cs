using Randevucum.Authentication.Orchestrator.Domain;

namespace Randevucum.Authentication.Orchestrator.API.Services.Adapters;

public static class GrpcContractAdapter
{
    #region gRPC Requests to Contract
    public static Contracts.Requests.ChangePasswordRequest ToContract(this ChangePasswordRequest request) => new(request.Token, request.CurrentPassword, request.NewPassword);
    public static Contracts.Requests.DeleteUserRequest ToContract(this DeleteUserRequest request) => new(request.Token);
    public static Contracts.Requests.EmailLoginRequest ToContract(this EmailLoginRequest request) => new(request.Email, request.Password);
    public static Contracts.Requests.ForgotPasswordRequest ToContract(this ForgotPasswordRequest request) => new(request.Email);
    public static Contracts.Requests.GetUserByIdRequest ToContract(this GetUserByIdRequest request) => new(request.UserId);
    public static Contracts.Requests.GetUserRequest ToContract(this GetUserRequest request) => new(request.Token);
    public static Contracts.Requests.GoogleLoginRequest ToContract(this GoogleLoginRequest request) => new(request.AuthenticationCode);
    public static Contracts.Requests.LogoutRequest ToContract(this LogoutRequest request) => new(request.Token);
    public static Contracts.Requests.RefreshTokenRequest ToContract(this RefreshTokenRequest request) => new(request.RefreshToken);
    public static Contracts.Requests.RegisterRequest ToContract(this RegisterRequest request) => new(request.Email, request.Password, request.Username, request.Role);
    public static Contracts.Requests.ResetPasswordRequest ToContract(this ResetPasswordRequest request) => new(request.ResetToken, request.NewPassword);
    public static Contracts.Requests.RoleCheckRequest ToContract(this RoleCheckRequest request) => new(request.Token, request.Role);
    public static Contracts.Requests.SendVerificationEmailRequest ToContract(this SendVerificationEmailRequest request) => new(request.Email);
    public static Contracts.Requests.SendForgotPasswordEmailRequest ToContract(this SendForgotPasswordEmailRequest request) => new(request.Email);
    public static Contracts.Requests.UpdateUserRequest ToContract(this UpdateUserRequest request) => new(request.Token, request.Username, request.Email, request.Role);
    public static Contracts.Requests.ValidateTokenRequest ToContract(this ValidateTokenRequest request) => new(request.Token);
    public static Contracts.Requests.VerifyEmailWithCodeRequest ToContract(this VerifyEmailWithCodeRequest request) => new(request.Code, request.VerificationCode);
    public static Contracts.Requests.VerifyEmailWithOneTimeCodeRequest ToContract(this VerifyEmailWithOneTimeCodeRequest request) => new(request.Code, request.VerificationCode);
    #endregion

    #region gRPC Responses to Contract
    public static Contracts.Responses.AuthResponse ToContract(this AuthResponse response) => new(response.OperationResult.ToContract(), response.AccessToken, response.RefreshToken);
    public static Contracts.Responses.ChangePasswordResponse ToContract(this ChangePasswordResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.DeleteUserResponse ToContract(this DeleteUserResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.ForgotPasswordResponse ToContract(this ForgotPasswordResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.GetUserResponse ToContract(this GetUserResponse response) => new(response.OperationResult.ToContract(), response.UserId, response.Username, response.Email, response.Role);
    public static Contracts.Responses.LogoutResponse ToContract(this LogoutResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.RegisterResponse ToContract(this RegisterResponse response) => new(response.OperationResult.ToContract(), response.UserId, response.Username, response.Email);
    public static Contracts.Responses.ResetPasswordResponse ToContract(this ResetPasswordResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.RoleCheckResponse ToContract(this RoleCheckResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.SendEmailResponse ToContract(this SendEmailResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.UpdateUserResponse ToContract(this UpdateUserResponse response) => new(response.OperationResult.ToContract());
    public static Contracts.Responses.ValidateTokenResponse ToContract(this ValidateTokenResponse response) => new(response.OperationResult.ToContract(), response.UserId);
    public static Contracts.Responses.VerifyEmailResponse ToContract(this VerifyEmailResponse response) => new(response.OperationResult.ToContract(), response.AuthResponse.ToContract());
    #endregion

    #region gRPC Common to Contract
    public static Contracts.Common.OperationResult ToContract(this OperationResult result) => new(result.Success, result.Message, result.StatusCode);
    #endregion

    #region Contract Request to gRPC Request
    public static ChangePasswordRequest ToGrpc(this Contracts.Requests.ChangePasswordRequest request) => new() { Token = request.Token, CurrentPassword = request.CurrentPassword, NewPassword = request.NewPassword };
    public static DeleteUserRequest ToGrpc(this Contracts.Requests.DeleteUserRequest request) => new() { Token = request.Token };
    public static EmailLoginRequest ToGrpc(this Contracts.Requests.EmailLoginRequest request) => new() { Email = request.Email, Password = request.Password };
    public static ForgotPasswordRequest ToGrpc(this Contracts.Requests.ForgotPasswordRequest request) => new() { Email = request.Email };
    public static GetUserByIdRequest ToGrpc(this Contracts.Requests.GetUserByIdRequest request) => new() { UserId = request.UserId };
    public static GetUserRequest ToGrpc(this Contracts.Requests.GetUserRequest request) => new() { Token = request.Token };
    public static GoogleLoginRequest ToGrpc(this Contracts.Requests.GoogleLoginRequest request) => new() { AuthenticationCode = request.AuthenticationCode };
    public static LogoutRequest ToGrpc(this Contracts.Requests.LogoutRequest request) => new() { Token = request.Token };
    public static RefreshTokenRequest ToGrpc(this Contracts.Requests.RefreshTokenRequest request) => new() { RefreshToken = request.RefreshToken };
    public static RegisterRequest ToGrpc(this Contracts.Requests.RegisterRequest request) => new() { Email = request.Email, Password = request.Password, Username = request.Username, Role = request.Role };
    public static ResetPasswordRequest ToGrpc(this Contracts.Requests.ResetPasswordRequest request) => new() { ResetToken = request.ResetToken, NewPassword = request.NewPassword };
    public static RoleCheckRequest ToGrpc(this Contracts.Requests.RoleCheckRequest request) => new() { Token = request.Token, Role = request.Role };
    public static SendVerificationEmailRequest ToGrpc(this Contracts.Requests.SendVerificationEmailRequest request) => new() { Email = request.Email };
    public static SendForgotPasswordEmailRequest ToGrpc(this Contracts.Requests.SendForgotPasswordEmailRequest request) => new() { Email = request.Email };
    public static UpdateUserRequest ToGrpc(this Contracts.Requests.UpdateUserRequest request) => new() { Token = request.Token, Username = request.Username, Email = request.Email, Role = request.Role };
    public static ValidateTokenRequest ToGrpc(this Contracts.Requests.ValidateTokenRequest request) => new() { Token = request.Token };
    public static VerifyEmailWithCodeRequest ToGrpc(this Contracts.Requests.VerifyEmailWithCodeRequest request) => new() { Code = request.Code, VerificationCode = request.VerificationCode };
    public static VerifyEmailWithOneTimeCodeRequest ToGrpc(this Contracts.Requests.VerifyEmailWithOneTimeCodeRequest request) => new() { Code = request.Code, VerificationCode = request.VerificationCode };
    #endregion

    #region Contract Response to gRPC Response
    public static AuthResponse ToGrpc(this Contracts.Responses.AuthResponse response) => new() { OperationResult = response.OperationResult.ToGrpc(), AccessToken = response.AccessToken, RefreshToken = response.RefreshToken };
    public static ChangePasswordResponse ToGrpc(this Contracts.Responses.ChangePasswordResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static DeleteUserResponse ToGrpc(this Contracts.Responses.DeleteUserResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static ForgotPasswordResponse ToGrpc(this Contracts.Responses.ForgotPasswordResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static GetUserResponse ToGrpc(this Contracts.Responses.GetUserResponse response) => new() { OperationResult = response.OperationResult.ToGrpc(), UserId = response.UserId, Username = response.Username, Email = response.Email, Role = response.Role };
    public static LogoutResponse ToGrpc(this Contracts.Responses.LogoutResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static RegisterResponse ToGrpc(this Contracts.Responses.RegisterResponse response) => new() { OperationResult = response.OperationResult.ToGrpc(), UserId = response.UserId, Username = response.Username, Email = response.Email };
    public static ResetPasswordResponse ToGrpc(this Contracts.Responses.ResetPasswordResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static RoleCheckResponse ToGrpc(this Contracts.Responses.RoleCheckResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static SendEmailResponse ToGrpc(this Contracts.Responses.SendEmailResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static UpdateUserResponse ToGrpc(this Contracts.Responses.UpdateUserResponse response) => new() { OperationResult = response.OperationResult.ToGrpc() };
    public static ValidateTokenResponse ToGrpc(this Contracts.Responses.ValidateTokenResponse response) => new() { OperationResult = response.OperationResult.ToGrpc(), UserId = response.UserId };
    public static VerifyEmailResponse ToGrpc(this Contracts.Responses.VerifyEmailResponse response) => new() { OperationResult = response.OperationResult.ToGrpc(), AuthResponse = response.AuthResponse.ToGrpc() };
    #endregion

    #region Contract Common to gRPC Common
    public static OperationResult ToGrpc(this Contracts.Common.OperationResult result) => new() { Success = result.Success, Message = result.Message, StatusCode = result.StatusCode };
    #endregion
}
