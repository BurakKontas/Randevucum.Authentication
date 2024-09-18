using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record AuthProviderAddedDomainEvent(Guid Id, UserId UserId, AuthProviderId AuthProviderId) : DomainEvent(Id);
