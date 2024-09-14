using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserPasswordChangedDomainEvent(Guid Id, UserId UserId, Password Password, Password NewPassword)
    : DomainEvent(Id)
{
    public UserPasswordChangedDomainEvent(UserId UserId, Password Password, Password NewPassword) : this(Guid.NewGuid(), UserId, Password, NewPassword)
    {
    }
}