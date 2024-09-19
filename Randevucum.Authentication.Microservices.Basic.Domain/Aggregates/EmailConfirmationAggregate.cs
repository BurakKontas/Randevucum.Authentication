using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class EmailConfirmationAggregate(EmailConfirmation emailConfirmation) : AggregateRoot
{
    public EmailConfirmation EmailConfirmation { get; init; } = emailConfirmation;

    public void ConfirmEmail()
    {
        if (EmailConfirmation.IsConfirmed)
            throw new InvalidOperationException("Email is already confirmed.");

        EmailConfirmation.Confirm();
    }
}