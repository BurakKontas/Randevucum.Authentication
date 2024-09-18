using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record UserActivityAddedDomainEvent(Guid Id, UserId UserId, UserActivityId ActivityId) : DomainEvent(Id);
