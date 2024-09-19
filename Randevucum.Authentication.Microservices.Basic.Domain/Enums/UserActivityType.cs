namespace Randevucum.Authentication.Microservices.Basic.Domain.Enums;

public enum UserActivityType
{
    Login,
    LoginAttempt,
    Register,
    PasswordResetRequest,
    PasswordReset,
    EmailConfirmation,
    PhoneConfirmation,
    ResendEmailConfirmation,
    ResendPhoneConfirmation,
    RefreshToken,
    Logout,
    ChangePassword,
    ChangeEmail,
    ChangePhoneNumber,
    AddAuthProvider
}