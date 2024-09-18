using Microsoft.AspNetCore.Http;

namespace Randevucum.Authentication.Common.Basic.Register;

public record UserRegisterResultedEvent(bool IsSuccess, string? Message = null, int StatusCode = StatusCodes.Status200OK);

public record UserRegisterSuccessEvent()
    : UserRegisterResultedEvent(true);

public record UserRegisterFailedEvent(string Message)
    : UserRegisterResultedEvent(false, Message, StatusCodes.Status400BadRequest);