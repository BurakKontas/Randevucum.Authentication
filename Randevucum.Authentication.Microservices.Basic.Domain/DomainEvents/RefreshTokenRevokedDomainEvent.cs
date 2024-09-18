using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record RefreshTokenRevokedDomainEvent(Guid Id, RefreshTokenId RefreshTokenId) : DomainEvent(Id);