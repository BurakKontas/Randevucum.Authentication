namespace Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

public static class CockroachOptionsSSLModeExtension
{
    public static string Value(this CockroachOptionsSSLMode sslMode)
    {
        return sslMode switch
        {
            CockroachOptionsSSLMode.Disable => "Disable",
            CockroachOptionsSSLMode.Enable => "Enable",
            _ => throw new ArgumentOutOfRangeException(nameof(sslMode), sslMode, null)
        };
    }
}