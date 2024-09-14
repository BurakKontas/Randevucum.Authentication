using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Repositories;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class UserAggregate(IUserRepository userRepository, User user = null)
{
    private readonly IUserRepository _userRepository = userRepository;
    public User User { get; private set; } = user;

    public void Register(string email, string password)
    {
        var existingUser = _userRepository.GetByEmail(new Email(email));
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email address is already in use.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            throw new ArgumentException("Password must be at least 6 characters long.");
        }

        User = new User(Guid.NewGuid(), new Email(email), new Password(password));
        _userRepository.Add(User);
    }

    public void ConfirmEmail()
    {
        if (User == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        User.EmailConfirmed = true;
        _userRepository.Update(User);
    }

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
        {
            throw new ArgumentException("Invalid email address.");
        }

        var email = new Email(newEmail);

        var existingUser = _userRepository.GetByEmail(email);
        if (existingUser != null && existingUser.Id != User.Id)
        {
            throw new InvalidOperationException("New email address is already in use.");
        }

        User.UpdateEmail(email);
        _userRepository.Update(User);
    }

    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
        {
            throw new ArgumentException("Password must be at least 6 characters long.");
        }

        User.UpdatePassword(new Password(newPassword));
        _userRepository.Update(User);
    }

    public void Login()
    {
        if (User == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        User.MarkAsLoggedIn();
        _userRepository.Update(User);
    }
}