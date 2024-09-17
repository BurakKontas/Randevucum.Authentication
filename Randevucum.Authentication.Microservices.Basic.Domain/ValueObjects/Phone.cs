using System.Text.RegularExpressions;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

public class Phone : ValueObject<Phone>
{
    private static readonly Regex PhoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled);
    public string Value { get; }

    public Phone(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid phone number", nameof(value));
        }
        Value = value;
    }

    private bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && PhoneRegex.IsMatch(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}