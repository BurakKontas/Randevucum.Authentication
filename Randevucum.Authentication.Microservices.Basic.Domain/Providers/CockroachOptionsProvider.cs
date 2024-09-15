using Randevucum.Authentication.Microservices.Basic.Domain.Options;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Providers;

public static class CockroachOptionsProvider
{
    private static CockroachOptions? _readOptions;
    private static CockroachOptions? _writeOptions;

    public static void Configure(CockroachOptions readOptions, CockroachOptions writeOptions)
    {
        _readOptions = readOptions;
        _writeOptions = writeOptions;
    }

    public static CockroachOptions? ReadOptions => _readOptions ?? throw new ArgumentNullException($"You need to configure CockroachOptionsProvider to use CockroachOptions.");
    public static CockroachOptions? WriteOptions => _writeOptions ?? throw new ArgumentNullException($"You need to configure CockroachOptionsProvider to use CockroachOptions.");
}