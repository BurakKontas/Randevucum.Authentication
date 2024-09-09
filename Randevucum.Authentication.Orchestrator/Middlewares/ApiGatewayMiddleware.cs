using System.Security.Cryptography;
using System.Text;
using Randevucum.Authentication.Orchestrator.Application.Strategies.Factories;

namespace Randevucum.Authentication.Orchestrator.API.Middlewares;

public class ApiGatewayMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly SecurityStrategyFactory _strategyFactory = new(configuration);

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsDebugMode())
        {
            await _next(context);
            return;
        }

        try
        {
            var protocol = context.Request.Protocol;
            var strategy = _strategyFactory.CreateStrategy(protocol);
            var result = strategy.CheckSecurityAsync(context);

            if (result.IsSafe)
            {
                await _next(context);
                return;
            }

            context.Response.StatusCode = result.StatusCode;
            if (!string.IsNullOrEmpty(result.Message))
            {
                await context.Response.WriteAsync(result.Message);
            }
        }
        catch (NotSupportedException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
    }

    private static bool IsDebugMode()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return string.IsNullOrEmpty(environment) || !string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase);
    }
}