namespace Randevucum.Authentication.Common.Basic.Login;

public record UserLoginReceivedEvent(
    string Email, 
    string Password,
    string AuthProvider,
    string IpAddress,
    string UserAgent
);