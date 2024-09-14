using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserEmailChangedDomainEvent(Guid Id, UserId UserId, Email Email, Email NewEmail) : DomainEvent(Id)
{
    public UserEmailChangedDomainEvent(UserId UserId, Email Email, Email NewEmail) : this(Guid.NewGuid(), UserId, Email, NewEmail)
    {
    }
}