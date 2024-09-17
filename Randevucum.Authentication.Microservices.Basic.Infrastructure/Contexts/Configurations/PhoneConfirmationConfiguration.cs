using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Contexts.Configurations;

public class PhoneConfirmationConfiguration : IEntityTypeConfiguration<PhoneConfirmation>
{
    public void Configure(EntityTypeBuilder<PhoneConfirmation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(phoneConfirmationId => phoneConfirmationId.Value, value => new PhoneConfirmationId(value));

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.PhoneNumber)
            .HasConversion(phoneNumber => phoneNumber.Value, value => new Phone(value));

        builder.Property(x => x.ConfirmationCode);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.ExpiresAt);

        builder.Property(x => x.IsConfirmed);

        builder.HasOne(pc => pc.User)
            .WithMany(u => u.PhoneConfirmations)
            .HasForeignKey(pc => pc.UserId)
            .IsRequired();
    }
}