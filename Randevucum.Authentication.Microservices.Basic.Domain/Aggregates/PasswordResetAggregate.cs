using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;

public class PasswordResetAggregate(PasswordResetRequest passwordResetRequest) : AggregateRoot
{
    public PasswordResetRequest PasswordResetRequest { get; init; } = passwordResetRequest;

    public void CompleteResetProcess()
    {
        if (PasswordResetRequest.IsUsed)
            throw new InvalidOperationException("Password reset request has already been used.");

        PasswordResetRequest.Use();
    }
}