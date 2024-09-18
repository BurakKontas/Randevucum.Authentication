using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record PasswordResetRequestedDomainEvent(Guid Id, UserId UserId, PasswordResetRequestId RequestId) : DomainEvent(Id);
