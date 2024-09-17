using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Contexts.Configurations;

public class EmailConfirmationsConfiguration : IEntityTypeConfiguration<EmailConfirmation>
{
    public void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(emailConfirmationId => emailConfirmationId.Value, value => new EmailConfirmationId(value));

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.ConfirmationToken)
            .HasMaxLength(512);

        builder.Property(x => x.ConfirmationCode)
            .HasMaxLength(512);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.ExpiresAt);

        builder.Property(x => x.IsConfirmed);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.ConfirmationToken)
            .IsUnique();

        builder.HasIndex(x => x.IsConfirmed);

        builder.HasOne(ec => ec.User)
            .WithMany(u => u.EmailConfirmations)
            .HasForeignKey(ec => ec.UserId)
            .IsRequired();
    }
}