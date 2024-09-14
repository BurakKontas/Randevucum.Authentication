using MediatR;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

public abstract record DomainEvent(Guid Id) : INotification;