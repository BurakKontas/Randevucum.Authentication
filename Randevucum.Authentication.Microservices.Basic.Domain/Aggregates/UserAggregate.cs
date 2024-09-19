using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class UserAggregate(User user, IpAddress ipAddress, UserAgent userAgent) : AggregateRoot
{
    public User User { get; init; } = user;
    public IpAddress IpAddress { get; init; } = ipAddress;
    public UserAgent UserAgent { get; init; } = userAgent;

    public ICollection<AuthProvider> AuthProviders => User.AuthProviders;
    public ICollection<BearerToken> BearerTokens => User.Tokens;
    public ICollection<RefreshToken> RefreshTokens => User.RefreshTokens;
    public ICollection<PasswordResetRequest> PasswordResetRequests => User.PasswordResetRequests;
    public ICollection<EmailConfirmation> EmailConfirmations => User.EmailConfirmations;
    public ICollection<PhoneConfirmation> PhoneConfirmations => User.PhoneConfirmations;
    public ICollection<UserActivity> UserActivities => User.UserActivities;

    public BearerToken? Login(string passwordHash)
    {
        UserActivity userActivity;
        if (!VerifyPassword(passwordHash))
        {
            userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.LoginAttempt);
            AddUserActivity(userActivity);
            return null;
        }

        //TODO: generate token and refresh token here then pass it to BearerToken.Create
        var refreshToken = RefreshToken.Create(new RefreshTokenId(Guid.NewGuid()), User.Id, "", IpAddress, UserAgent, DateTime.UtcNow.AddMinutes(15));
        var token = BearerToken.Create(new TokenId(Guid.NewGuid()), User.Id, "", IpAddress, UserAgent, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(15), refreshToken.Id);

        BearerTokens.Add(token);
        RefreshTokens.Add(refreshToken);

        userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.Login);
        AddUserActivity(userActivity);

        return token;
    }

    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrEmpty(newEmail))
            throw new ArgumentException("Email cannot be empty.");

        User.ChangeEmail(new Email(newEmail), DateTime.UtcNow);
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.ChangeEmail);
        AddUserActivity(userActivity);
    }

    public void ConfirmEmail()
    {
        if (User.IsEmailVerified)
            throw new InvalidOperationException("Email is already confirmed.");

        User.VerifyEmail();
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()),User.Id, UserActivityType.EmailConfirmation);
        AddUserActivity(userActivity);
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrEmpty(newPasswordHash))
            throw new ArgumentException("Password cannot be empty.");

        User.ChangePassword(new Password(newPasswordHash), DateTime.UtcNow);
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.ChangePassword);
        AddUserActivity(userActivity);
    }

    public void AddAuthProvider(AuthProviderList provider, string providerUserId)
    {
        if (AuthProviders.Any(p => p.ProviderName == provider))
            throw new InvalidOperationException($"{provider.Value()} is already added.");

        var authProviderId = new AuthProviderId(Guid.NewGuid());
        var authProvider = AuthProvider.Create(authProviderId, provider, User.Id, providerUserId);

        AuthProviders.Add(authProvider);
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.AddAuthProvider);
        AddUserActivity(userActivity);
    }

    public void AddPasswordResetRequest(PasswordResetRequest request)
    {
        PasswordResetRequests.Add(request);
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.PasswordResetRequest);
        AddUserActivity(userActivity);
    }

    private void AddUserActivity(UserActivity activity)
    {
        activity.AddBrowserIdentity(IpAddress, UserAgent);
        UserActivities.Add(activity);
    }

    private bool VerifyPassword(string passwordHash)
    {
        return User.PasswordHash.Verify(passwordHash);
    }
}
