using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class RefreshToken
{
    public RefreshTokenId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Token { get; private set; }
    public IpAddress IpAddress { get; private set; }
    public UserAgent UserAgent { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public DateTime? LoggedOutAt { get; private set; } 
    public bool IsLoggedOut => LoggedOutAt != default;
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    public bool IsRevoked => RevokedAt != default;

    public virtual User User { get; private set; }
    public virtual ICollection<BearerToken> BearerTokens { get; private set; }

    protected RefreshToken() { } // For EF Core

    private RefreshToken(RefreshTokenId id, UserId userId, string refreshToken, IpAddress ipAddress, UserAgent userAgent, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        Token = refreshToken;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        IssuedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        RevokedAt = null;
        LoggedOutAt = null;
    }

    public static RefreshToken Create(RefreshTokenId id, UserId userId, string refreshToken, IpAddress ipAddress, UserAgent userAgent, DateTime expiresAt)
    {
        return new RefreshToken(id, userId, refreshToken, ipAddress, userAgent, expiresAt);
    }

    public void Extend(DateTime newExpiresAt)
    {
        ExpiresAt = newExpiresAt;
    }

    public void Revoke(DateTime revokedAt)
    {
        RevokedAt = revokedAt;
    }

    public void RemoveAllBearerTokens()
    {
        BearerTokens.Clear();
    }
}