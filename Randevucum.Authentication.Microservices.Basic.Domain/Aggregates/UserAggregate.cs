using Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class UserAggregate(User user) : AggregateRoot
{
    public User User { get; init; } = user;
    public ICollection<AuthProvider> AuthProviders => User.AuthProviders;
    public ICollection<BearerToken> BearerTokens => User.Tokens;
    public ICollection<RefreshToken> RefreshTokens => User.RefreshTokens;
    public ICollection<PasswordResetRequest> PasswordResetRequests => User.PasswordResetRequests;
    public ICollection<EmailConfirmation> EmailConfirmations => User.EmailConfirmations;
    public ICollection<PhoneConfirmation> PhoneConfirmations => User.PhoneConfirmations;
    public ICollection<UserActivity> UserActivities => User.UserActivities;

    public void ConfirmEmail()
    {
        if (User.IsEmailVerified)
            throw new InvalidOperationException("Email is already confirmed.");

        User.VerifyEmail();
        Arise(new EmailConfirmedDomainEvent(Guid.NewGuid(), User.Id));
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrEmpty(newPasswordHash))
            throw new ArgumentException("Password cannot be empty.");

        User.ChangePassword(new Password(newPasswordHash), DateTime.UtcNow);
        Arise(new PasswordChangedDomainEvent(Guid.NewGuid(), User.Id));
    }

    public void AddUserActivity(UserActivity activity)
    {
        User.UserActivities.Add(activity);
        Arise(new UserActivityAddedDomainEvent(Guid.NewGuid(), User.Id, activity.Id));
    }

    public void AddAuthProvider(AuthProvider authProvider)
    {
        if (AuthProviders.Any(p => p.ProviderName == authProvider.ProviderName))
            throw new InvalidOperationException($"{authProvider.ProviderName} is already added.");

        User.AuthProviders.Add(authProvider);
        Arise(new AuthProviderAddedDomainEvent(Guid.NewGuid(), User.Id, authProvider.Id));
    }

    public void AddPasswordResetRequest(PasswordResetRequest request)
    {
        User.PasswordResetRequests.Add(request);
        Arise(new PasswordResetRequestedDomainEvent(Guid.NewGuid(), User.Id, request.Id));
    }

    public void RemovePasswordResetRequest(PasswordResetRequest request)
    {
        User.PasswordResetRequests.Remove(request);
        Arise(new PasswordResetRequestRemovedDomainEvent(Guid.NewGuid(), User.Id, request.Id));
    }
}
