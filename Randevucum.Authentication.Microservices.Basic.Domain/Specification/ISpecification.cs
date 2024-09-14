namespace Randevucum.Authentication.Microservices.Basic.Domain.Specification;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T candidate);
}