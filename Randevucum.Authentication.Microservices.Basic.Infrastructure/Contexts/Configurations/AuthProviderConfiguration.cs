using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Contexts.Configurations;

public class AuthProviderConfiguration : IEntityTypeConfiguration<AuthProvider>
{
    public void Configure(EntityTypeBuilder<AuthProvider> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(authProviderId => authProviderId.Value, value => new AuthProviderId(value));

        builder.Property(x => x.ProviderName)
            .HasConversion(authProviderList => authProviderList.Value(), value => value.ToAuthProviderList());

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.ProviderName);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.ProviderName);

        builder.HasIndex(x => new { x.UserId, x.ProviderName }).IsUnique();

        builder.HasOne(ap => ap.User)
            .WithMany(u => u.AuthProviders)
            .HasForeignKey(ap => ap.UserId)
            .IsRequired();
    }
}