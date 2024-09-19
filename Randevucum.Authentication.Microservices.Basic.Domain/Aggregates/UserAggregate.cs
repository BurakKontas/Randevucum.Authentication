using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class UserAggregate : AggregateRoot
{
    private User? _user;
    public User User
    {
        get => _user ?? throw new InvalidOperationException("User is not found.");
        private set => _user = value;
    }

    public IpAddress IpAddress { get; init; }
    public UserAgent UserAgent { get; init; }

    public ICollection<AuthProvider> AuthProviders => User.AuthProviders ?? throw new InvalidOperationException("User is not found.");
    public ICollection<BearerToken> BearerTokens => User.Tokens;
    public ICollection<RefreshToken> RefreshTokens => User.RefreshTokens;
    public ICollection<PasswordResetRequest> PasswordResetRequests => User.PasswordResetRequests;
    public ICollection<EmailConfirmation> EmailConfirmations => User.EmailConfirmations;
    public ICollection<PhoneConfirmation> PhoneConfirmations => User.PhoneConfirmations;
    public ICollection<UserActivity> UserActivities => User.UserActivities;

    public UserAggregate(IpAddress ipAddress, UserAgent userAgent)
    {
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }

    public UserAggregate(User user, IpAddress ipAddress, UserAgent userAgent)
    {
        User = user;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }

    public User Register(Email email, Password password, AuthProviderList provider, string providerUserId, bool isEmailVerified, bool isPhoneVerified)
    {
        if (_user is not null)
            throw new InvalidOperationException("User is already registered.");

        var userId = new UserId(Guid.NewGuid());
        User = User.Create(userId, email, password, isEmailVerified);
        AddAuthProvider(provider, providerUserId);

        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.Register);
        AddUserActivity(userActivity);

        return User;
    }


    public BearerToken? Login(string passwordHash)
    {
        if (User is null)
            throw new InvalidOperationException("User is not found.");

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
        if (User is null)
            throw new InvalidOperationException("User is not found.");

        if (string.IsNullOrEmpty(newEmail))
            throw new ArgumentException("Email cannot be empty.");

        User.ChangeEmail(new Email(newEmail), DateTime.UtcNow);
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.ChangeEmail);
        AddUserActivity(userActivity);
    }

    public void ConfirmEmail()
    {
        if (User is null)
            throw new InvalidOperationException("User is not found.");

        if (User.IsEmailVerified)
            throw new InvalidOperationException("Email is already confirmed.");

        User.VerifyEmail();
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()),User.Id, UserActivityType.EmailConfirmation);
        AddUserActivity(userActivity);
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (User is null)
            throw new InvalidOperationException("User is not found.");

        if (string.IsNullOrEmpty(newPasswordHash))
            throw new ArgumentException("Password cannot be empty.");

        User.ChangePassword(new Password(newPasswordHash), DateTime.UtcNow);
        var userActivity = UserActivity.Create(new UserActivityId(Guid.NewGuid()), User.Id, UserActivityType.ChangePassword);
        AddUserActivity(userActivity);
    }

    public void AddAuthProvider(AuthProviderList provider, string providerUserId)
    {
        if (User is null)
            throw new InvalidOperationException("User is not found.");

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
        if (User is null)
            throw new InvalidOperationException("User is not found.");

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
        return User!.PasswordHash.Verify(passwordHash);
    }
}
