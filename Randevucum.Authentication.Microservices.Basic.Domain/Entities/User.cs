using Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.Specification;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Entities;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public bool EmailConfirmed { get; set; }
    public DateTime LastLogin { get; private set; }

    private User(UserId id, Email email, Password password)
    {
        Id = id;
        Email = email;
        Password = password;
        EmailConfirmed = false;
        LastLogin = DateTime.MinValue;
    }

    public static User Create(UserId id, Email email, Password password)
    {
        return new User(id, email, password);
    }

    public static User Create(Email email, Password password)
    {
        return new User(new UserId(Guid.NewGuid()), email, password);
    }

    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail;
    }

    public void UpdatePassword(Password newPassword)
    {
        Password = newPassword;
    }

    public void MarkAsLoggedIn()
    {
        LastLogin = DateTime.UtcNow;
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
    }

    public bool CheckIfEmailIsConfirmed(ISpecification<User> specification)
    {
        return specification.IsSatisfiedBy(this);
    }
}