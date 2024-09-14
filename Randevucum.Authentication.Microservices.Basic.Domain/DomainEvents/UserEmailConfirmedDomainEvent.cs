using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserEmailConfirmedDomainEvent(Guid Id, UserId UserId, Email Email) : DomainEvent(Id)
{
    public UserEmailConfirmedDomainEvent(UserId UserId, Email Email) : this(Guid.NewGuid(), UserId, Email)
    {
    }
}