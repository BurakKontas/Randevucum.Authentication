using Microsoft.Extensions.Configuration;
using Randevucum.Authentication.Orchestrator.Domain.Interfaces;

namespace Randevucum.Authentication.Orchestrator.Application.Strategies.Factories;

public class SecurityStrategyFactory(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public ISecurityStrategy? CreateStrategy(string protocol)
    {
        return protocol switch
        {
            "HTTP/2" => new Http2SecurityStrategy(),
            "HTTP/1.1" => new HttpSecurityStrategy(_configuration),
            _ => null
        };
    }
}
