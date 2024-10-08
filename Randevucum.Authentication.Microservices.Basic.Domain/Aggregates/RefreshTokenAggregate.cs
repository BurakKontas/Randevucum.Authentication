﻿using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class RefreshTokenAggregate(RefreshToken refreshToken) : AggregateRoot
{
    public RefreshToken RefreshToken { get; init; } = refreshToken;

    public void ExtendExpiration(DateTime newExpirationDate)
    {
        if (newExpirationDate <= RefreshToken.ExpiresAt)
            throw new InvalidOperationException("New expiration date must be later than the current one.");

        RefreshToken.Extend(newExpirationDate);
    }

    public void RevokeToken()
    {
        if (RefreshToken.IsRevoked)
            throw new InvalidOperationException("Token is already revoked.");

        RefreshToken.Revoke(DateTime.UtcNow);
    }

    public void RemoveAllBearerTokens()
    {
        if(RefreshToken.BearerTokens.Count == 0)
            throw new InvalidOperationException("No bearer tokens to remove.");

        RefreshToken.RemoveAllBearerTokens();
    }
}