using Microsoft.Extensions.Options;
using Randevucum.Authentication.Microservices.Basic.Domain.Options;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Providers;

public class CockroachOptionsProvider
{
    private readonly IOptionsMonitor<CockroachOptions> _optionsMonitor;

    public CockroachOptionsProvider(IOptionsMonitor<CockroachOptions> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
        UpdateOptions();
        _optionsMonitor.OnChange(UpdateOptions);
    }

    private void UpdateOptions(CockroachOptions? options = null)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));

        var readOptions = _optionsMonitor.Get("CockroachRead");
        var writeOptions = _optionsMonitor.Get("CockroachWrite");
        Configure(readOptions, writeOptions);
    }

    private static CockroachOptions? _readOptions;
    private static CockroachOptions? _writeOptions;

    public static void Configure(CockroachOptions readOptions, CockroachOptions writeOptions)
    {
        _readOptions = readOptions;
        _writeOptions = writeOptions;
    }

    public static CockroachOptions? Read => _readOptions ?? throw new ArgumentNullException($"You need to configure CockroachOptionsProvider to use CockroachOptions.");
    public static CockroachOptions? Write => _writeOptions ?? throw new ArgumentNullException($"You need to configure CockroachOptionsProvider to use CockroachOptions.");
}