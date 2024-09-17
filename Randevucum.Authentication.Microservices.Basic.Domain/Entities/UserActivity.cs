using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class UserActivity
{
    public UserActivityId Id { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserActivityType ActivityType { get; private set; }

    public virtual User User { get; private set; }

    protected UserActivity() { } // For EF Core

    private UserActivity(UserActivityId id, UserId userId, UserActivityType activityType)
    {
        Id = id;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        ActivityType = activityType;
    }

    public static UserActivity Create(UserActivityId id, UserId userId, UserActivityType activityType)
    {
        return new UserActivity(id, userId, activityType);
    }
}