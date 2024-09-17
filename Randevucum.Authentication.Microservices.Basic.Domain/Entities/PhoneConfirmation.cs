using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class PhoneConfirmation
{
    public PhoneConfirmationId Id { get; private set; }
    public UserId UserId { get; private set; }
    public Phone PhoneNumber { get; private set; }
    public string ConfirmationCode { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    protected PhoneConfirmation() { } // For EF Core

    private PhoneConfirmation(PhoneConfirmationId id, UserId userId, Phone phoneNumber, string confirmationCode, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        PhoneNumber = phoneNumber;
        ConfirmationCode = confirmationCode;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        IsConfirmed = false;
    }

    public static PhoneConfirmation Create(PhoneConfirmationId id, UserId userId, Phone phoneNumber, string confirmationCode, DateTime expiresAt)
    {
        return new PhoneConfirmation(id, userId, phoneNumber, confirmationCode, expiresAt);
    }

    public void Confirm()
    {
        IsConfirmed = true;
    }
}