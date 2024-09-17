using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

public class IpAddress : ValueObject<IpAddress>
{
    public string Value { get; }

    public IpAddress(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid IP address", nameof(value));
        }
        Value = value;
    }

    private bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && System.Net.IPAddress.TryParse(value, out _);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}