using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class BearerToken
{
    public TokenId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Token { get; private set; }
    public RefreshTokenId? RefreshTokenId { get; private set; }
    public IpAddress IpAddress { get; private set; }
    public UserAgent UserAgent { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    public virtual User User { get; private set; }
    public virtual RefreshToken RefreshToken { get; private set; }

    protected BearerToken() { } // For EF Core

    private BearerToken(TokenId id, UserId userId, string token, IpAddress ipAddress, UserAgent userAgent, DateTime issuedAt, DateTime expiresAt, RefreshTokenId? refreshTokenId)
    {
        Id = id;
        UserId = userId;
        Token = token;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        IssuedAt = issuedAt;
        ExpiresAt = expiresAt;
        RefreshTokenId = refreshTokenId;
    }

    public static BearerToken Create(TokenId id, UserId userId, string token, IpAddress ipAddress, UserAgent userAgent, DateTime issuedAt, DateTime expiresAt, RefreshTokenId? refreshTokenId)
    {
        return new BearerToken(id, userId, token, ipAddress, userAgent, issuedAt, expiresAt, refreshTokenId);
    }
}