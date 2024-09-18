using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class PasswordResetRequest
{
    public PasswordResetRequestId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsUsed { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    public virtual User User { get; private set; }

    protected PasswordResetRequest() { } // For EF Core

    private PasswordResetRequest(PasswordResetRequestId id, UserId userId, string token, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        Token = token;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        IsUsed = false;
    }

    public static PasswordResetRequest Create(PasswordResetRequestId id, UserId userId, string token, DateTime expiresAt)
    {
        return new PasswordResetRequest(id, userId, token, expiresAt);
    }

    public void Use()
    {
        IsUsed = true;
    }
}