using Microsoft.AspNetCore.Http;

namespace Randevucum.Authentication.Common.OAuth.GoogleLogin;

public record GoogleLoginResultedEvent(bool IsSuccess, string Token, string RefreshToken, string? Message = null, int StatusCode = StatusCodes.Status200OK);