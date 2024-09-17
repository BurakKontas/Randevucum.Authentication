namespace Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

public static class CockroachOptionsSSLModeExtension
{
    public static string Value(this CockroachOptionsSSLMode sslMode)
    {
        var name = Enum.GetName(typeof(CockroachOptionsSSLMode), sslMode);

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentOutOfRangeException(nameof(sslMode), sslMode, null);
        }

        return name;
    }
}