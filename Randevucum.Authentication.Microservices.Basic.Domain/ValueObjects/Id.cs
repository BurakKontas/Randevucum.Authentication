using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;
using System.Text.RegularExpressions;

namespace Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;


public class Id(Guid value) : ValueObject<Id>
{
    public Guid Value { get; } = value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}