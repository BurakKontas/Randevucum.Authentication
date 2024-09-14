using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserRegisteredDomainEvent(Guid Id, UserId UserId) : DomainEvent(Id)
{
    public UserRegisteredDomainEvent(UserId UserId) : this(Guid.NewGuid(), UserId) 
    {
    }
}