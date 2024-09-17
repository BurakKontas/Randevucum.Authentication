using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class EmailConfirmation
{
    public EmailConfirmationId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string ConfirmationCode { get; private set; }
    public string ConfirmationToken { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    protected EmailConfirmation() { } // For EF Core

    private EmailConfirmation(EmailConfirmationId id, UserId userId, string confirmationCode, string confirmationToken, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        ConfirmationCode = confirmationCode;
        ConfirmationToken = confirmationToken;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        IsConfirmed = false;
    }

    public static EmailConfirmation Create(EmailConfirmationId id, UserId userId, string confirmationCode, string confirmationToken, DateTime expiresAt)
    {
        return new EmailConfirmation(id, userId, confirmationCode, confirmationToken, expiresAt);
    }

    public void Confirm()
    {
        IsConfirmed = true;
    }
}