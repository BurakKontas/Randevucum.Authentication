﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.Email)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(x => x.PasswordHash)
            .HasConversion(passwordHash => passwordHash.Value, value => new Password(value));

        builder.Property(x => x.Phone)
            .HasConversion(
                phone => phone! != null! ? phone.Value : null,
                value => value != null ? new Phone(value) : null
            );

        builder.Property(x => x.IsEmailVerified);

        builder.Property(x => x.IsPhoneVerified);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.Phone)
            .IsUnique();

        builder.HasMany(u => u.AuthProviders)
            .WithOne(ap => ap.User)
            .HasForeignKey(ap => ap.UserId);

        builder.HasMany(u => u.Tokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        builder.HasMany(u => u.EmailConfirmations)
            .WithOne(ec => ec.User)
            .HasForeignKey(ec => ec.UserId);

        builder.HasMany(u => u.PasswordResetRequests)
            .WithOne(prr => prr.User)
            .HasForeignKey(prr => prr.UserId);

        builder.HasMany(u => u.PhoneConfirmations)
            .WithOne(pc => pc.User)
            .HasForeignKey(pc => pc.UserId);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId);

        builder.HasMany(u => u.UserActivities)
            .WithOne(ua => ua.User)
            .HasForeignKey(ua => ua.UserId);
    }
}