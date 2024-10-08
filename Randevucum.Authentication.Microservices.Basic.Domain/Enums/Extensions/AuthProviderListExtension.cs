﻿namespace Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

public static class AuthProviderListExtension
{
    public static string Value(this AuthProviderList authProvider)
    {
        var name = Enum.GetName(typeof(AuthProviderList), authProvider);

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentOutOfRangeException(nameof(authProvider), authProvider, null);
        }

        return name;
    }

    public static AuthProviderList ToAuthProviderList(this string value)
    {
        if (Enum.TryParse<AuthProviderList>(value, true, out var authProvider))
        {
            return authProvider;
        }

        throw new ArgumentOutOfRangeException(nameof(value), value, null);
    }
}