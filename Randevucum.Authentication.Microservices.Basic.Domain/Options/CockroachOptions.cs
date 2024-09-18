// ReSharper disable InconsistentNaming
using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Options;

public class CockroachOptions
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string Database { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required CockroachOptionsSSLMode SSLMode { get; set; }

    public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};SSL Mode={SSLMode.Value()};Trust Server Certificate=True";
}