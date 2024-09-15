using Microsoft.Extensions.Options;
using Randevucum.Authentication.Microservices.Basic.Domain.Options;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Providers;

public class CockroachOptionsProvider
{
    private readonly IOptionsMonitor<CockroachOptions> _optionsMonitor;
    private CockroachOptions _readOptions = null!;
    private CockroachOptions _writeOptions = null!;

    public CockroachOptionsProvider(IOptionsMonitor<CockroachOptions> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
        UpdateOptions();
        _optionsMonitor.OnChange(UpdateOptions);
    }

    private void UpdateOptions(CockroachOptions? options = null)
    {
        _readOptions = _optionsMonitor.Get("CockroachRead");
        _writeOptions = _optionsMonitor.Get("CockroachWrite");
    }

    public CockroachOptions Read => _readOptions ?? throw new ArgumentNullException($"CockroachRead options not configured.");
    public CockroachOptions Write => _writeOptions ?? throw new ArgumentNullException($"CockroachWrite options not configured.");
}