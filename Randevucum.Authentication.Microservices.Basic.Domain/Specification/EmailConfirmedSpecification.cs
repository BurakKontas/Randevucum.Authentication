using Randevucum.Authentication.Microservices.Basic.Domain.Entities;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Specification;

public class EmailConfirmedSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.EmailConfirmed;
    }
}