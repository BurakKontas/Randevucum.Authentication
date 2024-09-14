using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserLoggedInDomainEvent(Guid Id, UserId UserId) : DomainEvent(Id)
{
    public UserLoggedInDomainEvent(UserId UserId) : this(Guid.NewGuid(), UserId) 
    { }
}