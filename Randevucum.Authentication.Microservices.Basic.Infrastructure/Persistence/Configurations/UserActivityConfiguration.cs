using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence.Configurations;

public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
{
    public void Configure(EntityTypeBuilder<UserActivity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new UserActivityId(value));

        builder.Property(x => x.UserId)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(x => x.ActivityType)
            .HasConversion(activityType => activityType.Value(), value => value.ToUserActivityType());

        builder.Property(x => x.CreatedAt);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.ActivityType);

        builder.HasOne(ua => ua.User)
            .WithMany(u => u.UserActivities)
            .HasForeignKey(ua => ua.UserId)
            .IsRequired();
    }
}