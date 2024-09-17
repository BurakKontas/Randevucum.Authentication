using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class AuthProvider
{
    public AuthProviderId Id { get; private set; }
    public AuthProviderList ProviderName { get; private set; }
    public UserId UserId { get; private set; }
    public string ProviderUserId { get; private set; }

    public virtual User User { get; private set; }

    protected AuthProvider() { } // For EF Core

    private AuthProvider(AuthProviderId id, AuthProviderList providerName, UserId userId, string providerUserId)
    {
        Id = id;
        ProviderName = providerName;
        UserId = userId;
        ProviderUserId = providerUserId;
    }

    public static AuthProvider Create(AuthProviderId id, AuthProviderList providerName, UserId userId, string providerUserId)
    {
        return new AuthProvider(id, providerName, userId, providerUserId);
    }
}