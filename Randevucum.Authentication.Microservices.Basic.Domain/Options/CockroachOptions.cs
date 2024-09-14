// ReSharper disable InconsistentNaming
using Randevucum.Authentication.Microservices.Basic.Domain.Enums;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Options;

public class CockroachOptions(string host, int port, string database, string username, string password, CockroachOptionsSSLMode sslMode = CockroachOptionsSSLMode.Disable)
{
    private string Host { get; } = host;
    private int Port { get; } = port;
    private string Database { get; } = database;
    private string Username { get; } = username;
    private string Password { get; } = password;
    private CockroachOptionsSSLMode SSLMode { get; } = sslMode;

    public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};SSL Mode={SSLMode.Value()}";
}