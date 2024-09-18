using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.DomainEvents;

public record PhoneConfirmedDomainEvent(Guid Id, UserId UserId, PhoneConfirmationId PhoneConfirmationId) : DomainEvent(Id);