using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Phone? Phone { get; private set; }
    public Password PasswordHash { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public bool IsPhoneVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public virtual ICollection<AuthProvider> AuthProviders { get; private set; }
    public virtual ICollection<BearerToken> Tokens { get; private set; }
    public virtual ICollection<EmailConfirmation> EmailConfirmations { get; private set; }
    public virtual ICollection<PasswordResetRequest> PasswordResetRequests { get; private set; }
    public virtual ICollection<PhoneConfirmation> PhoneConfirmations { get; private set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; private set; }
    public virtual ICollection<UserActivity> UserActivities { get; private set; }

    protected User() { } // For EF Core

    private User(UserId id, Email email, Password passwordHash, bool isEmailVerified, DateTime createdAt, DateTime updatedAt, Phone? phone, bool isPhoneVerified)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        IsEmailVerified = isEmailVerified;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Phone = phone;
        IsPhoneVerified = isPhoneVerified;
    }

    public static User Create(UserId id, Email email, Password passwordHash, bool isEmailVerified, DateTime createdAt, DateTime updatedAt, Phone? phone, bool isPhoneVerified)
    {
        return new User(id, email, passwordHash, isEmailVerified, createdAt, updatedAt, phone, isPhoneVerified);
;    }
    
    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void ChangePassword(Password passwordHash, DateTime updatedAt)
    {
        PasswordHash = passwordHash;
        UpdatedAt = updatedAt;
    }

    public void ChangeEmail(Email email, DateTime updatedAt)
    {
        Email = email;
        UpdatedAt = updatedAt;
    }

    public void ChangePhone(Phone phone, DateTime updatedAt)
    {
        Phone = phone;
        UpdatedAt = updatedAt;
    }

    public void VerifyPhone()
    {
        IsPhoneVerified = true;
    }
}