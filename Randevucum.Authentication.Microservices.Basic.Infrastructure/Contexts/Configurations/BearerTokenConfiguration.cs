﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Contexts.Configurations;

public class BearerTokenConfiguration : IEntityTypeConfiguration<BearerToken>
{
    public void Configure(EntityTypeBuilder<BearerToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(tokenId => tokenId.Value, value => new TokenId(value));

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.Token)
            .HasMaxLength(512);

        builder.Property(x => x.RefreshTokenId)
            .HasConversion(
                refreshTokenId => refreshTokenId != null! ? refreshTokenId.Value : (Guid?)null,
                value => value != null ? new RefreshTokenId(value.Value) : (RefreshTokenId?)null
            );

        builder.Property(x => x.IpAddress)
            .HasConversion(ipAddress => ipAddress.Value, value => new IpAddress(value));

        builder.Property(x => x.UserAgent)
            .HasConversion(userAgent => userAgent.Value, value => new UserAgent(value));

        builder.Property(x => x.IssuedAt);

        builder.Property(x => x.ExpiresAt);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Token);
    }
}