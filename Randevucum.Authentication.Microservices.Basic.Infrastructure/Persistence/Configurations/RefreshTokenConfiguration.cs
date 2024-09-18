using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(refreshTokenId => refreshTokenId.Value, value => new RefreshTokenId(value));

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.Token)
            .HasMaxLength(512);

        builder.Property(x => x.IpAddress)
            .HasConversion(ipAddress => ipAddress.Value, value => new IpAddress(value));

        builder.Property(x => x.UserAgent)
            .HasConversion(userAgent => userAgent.Value, value => new UserAgent(value));

        builder.Property(x => x.IssuedAt);

        builder.Property(x => x.ExpiresAt);

        builder.Property(x => x.RevokedAt);

        builder.Property(x => x.LoggedOutAt);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Token)
            .IsUnique();

        builder.HasIndex(x => x.IpAddress);

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .IsRequired();

        builder.HasMany(rt => rt.BearerTokens)
            .WithOne(bt => bt.RefreshToken)
            .HasForeignKey(bt => bt.RefreshTokenId)
            .IsRequired();
    }
}