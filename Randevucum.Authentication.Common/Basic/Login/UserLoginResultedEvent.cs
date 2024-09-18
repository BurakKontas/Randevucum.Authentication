using Microsoft.AspNetCore.Http;

namespace Randevucum.Authentication.Common.Basic.Login;

public record UserLoginResultedEvent(bool IsSuccess, string Token, string RefreshToken, string? Message = null, int StatusCode = StatusCodes.Status200OK);

public record UserLoginSuccessEvent(string Token, string RefreshToken)
    : UserLoginResultedEvent(true, Token, RefreshToken);

public record UserLoginFailedEvent(string Message)
    : UserLoginResultedEvent(false, "", "", Message, StatusCodes.Status400BadRequest);