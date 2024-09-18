namespace Randevucum.Authentication.Common.Basic.Login;

public record UserLoginRequestedEvent(string Email, string Password);