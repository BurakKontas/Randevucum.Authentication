using Microsoft.AspNetCore.Http;

namespace Randevucum.Authentication.Common.OAuth.GoogleLogin;

public record GoogleLoginResultedEvent(bool IsSuccess, string Token, string RefreshToken, string? Message = null, int StatusCode = StatusCodes.Status200OK);

public record GoogleLoginSuccessEvent(string Token, string RefreshToken)
    : GoogleLoginResultedEvent(true, Token, RefreshToken);

public record GoogleLoginFailedEvent(string Message)
    : GoogleLoginResultedEvent(false, "", "", Message, StatusCodes.Status400BadRequest);