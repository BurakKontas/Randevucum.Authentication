using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Contexts.Configurations;

public class PasswordResetRequestConfiguration : IEntityTypeConfiguration<PasswordResetRequest>
{
    public void Configure(EntityTypeBuilder<PasswordResetRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(passwordResetRequestId => passwordResetRequestId.Value, value => new PasswordResetRequestId(value));

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.Token)
            .HasMaxLength(512);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.ExpiresAt);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Token)
            .IsUnique();
    }
}