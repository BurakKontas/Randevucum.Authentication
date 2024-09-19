using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class PhoneConfirmationAggregate(PhoneConfirmation phoneConfirmation) : AggregateRoot
{
    public PhoneConfirmation PhoneConfirmation { get; init; } = phoneConfirmation;

    public void ConfirmPhone()
    {
        if (PhoneConfirmation.IsConfirmed)
            throw new InvalidOperationException("Phone is already confirmed.");

        PhoneConfirmation.Confirm();
    }
}