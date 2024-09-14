using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

public class Email : ValueObject<Email>
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
        {
            throw new ArgumentException("Email is not valid.");
        }

        Value = value;
    }

    private static bool IsValidEmail(string email)
    {
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
