using System.Diagnostics;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

public static class UserActivityTypeExtension
{
    public static string Value(this UserActivityType userActivityType)
    {
        var name = Enum.GetName(typeof(UserActivityType), userActivityType);

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentOutOfRangeException(nameof(userActivityType), userActivityType, null);
        }

        return name;
    }

    public static UserActivityType ToUserActivityType(this string value)
    {
        if (Enum.TryParse<UserActivityType>(value, true, out var userActivityType))
        {
            return userActivityType;
        }

        throw new ArgumentOutOfRangeException(nameof(value), value, null);
    }
}