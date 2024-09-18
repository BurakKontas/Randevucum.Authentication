namespace Randevucum.Authentication.Common.Basic.Register;

public record UserRegisterRequestedEvent(string Email, string Password, string? Phone, bool IsEmailVerified = false, bool IsPhoneVerified = false);