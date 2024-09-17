using System.Text.RegularExpressions;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

public class UserAgent : ValueObject<UserAgent>
{
    private static readonly Regex UserAgentRegex = new Regex(@"^[a-zA-Z0-9\s\.\(\);\/-]+$", RegexOptions.Compiled);

    public string Value { get; }

    public UserAgent(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid User-Agent", nameof(value));
        }
        Value = value;
    }

    private bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && UserAgentRegex.IsMatch(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}