﻿using System;
using System.Collections.Generic;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

public class Password : ValueObject<Password>
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
        {
            throw new ArgumentException("Şifre en az 6 karakter olmalıdır.");
        }

        Value = value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
