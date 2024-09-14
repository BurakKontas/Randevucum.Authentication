using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserLoggedInDomainEvent(Guid Id, UserId UserId, DateTime DateTime) : DomainEvent(Id)
{
    public UserLoggedInDomainEvent(UserId UserId, DateTime DateTime) : this(Guid.NewGuid(), UserId, DateTime) 
    { }
}