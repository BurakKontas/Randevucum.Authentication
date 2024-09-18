namespace Randevucum.Authentication.Common.Basic.Register;

public record UserRegisterReceivedEvent(string Email, string Password, string? Phone, bool IsEmailVerified = false, bool IsPhoneVerified = false);