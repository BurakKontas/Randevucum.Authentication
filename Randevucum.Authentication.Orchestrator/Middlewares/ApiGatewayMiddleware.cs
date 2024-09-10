using System.Security.Cryptography;
using System.Text;
using Randevucum.Authentication.Orchestrator.Application.Strategies.Factories;
using Randevucum.Authentication.Orchestrator.Domain.Interfaces;

namespace Randevucum.Authentication.Orchestrator.API.Middlewares;

public class ApiGatewayMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsDebugMode())
        {
            await _next(context);
            return;
        }

        if (IsHttp2(context))
        {
            await _next(context);
            return;
        }

        // after this point, we are sure that the request is HTTP/1.1 and probably graphql

        var salt = configuration.GetSection("TraceIdentifierSalt").Value;

        if (salt is null)
        {
            throw new ArgumentNullException(nameof(salt), "TraceIdentifierSalt is not set in appsettings.json");
        }

        var isTraceAvailable = context.Request.Headers.TryGetValue("OcRequestId", out var trace);
        var isTraceHashAvailable = context.Request.Headers.TryGetValue("X-Trace-Hash", out var traceHash);

        if (!isTraceAvailable || !isTraceHashAvailable)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("OcRequestId or X-Trace-Hash missing.");
            return;
        }

        var compareHash = CalculateSha256WithSalt(trace!, salt);

        if (!compareHash.Equals(traceHash))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid X-Trace-Hash value.");
            return;
        }

        await _next(context);
    }

    private static string CalculateSha256WithSalt(string input, string salt)
    {
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var inputBytes = Encoding.UTF8.GetBytes(input);

        var combinedBytes = new byte[inputBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(inputBytes, 0, combinedBytes, 0, inputBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, inputBytes.Length, saltBytes.Length);

        return Convert.ToBase64String(SHA256.HashData(combinedBytes));
    }

    private static bool IsHttp2(HttpContext context)
    {
        if (context.Request.Protocol.Equals("HTTP/2"))
        {
            context.Request.Headers.TryGetValue("X-Hash", out var hash);

            //TODO: sadece mikroservislerin bileceği ve headerde ileteceği bir key oluşturulmalı ve kontrol edilmeli

            return true;
        }
    }

    private static bool IsDebugMode()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return string.IsNullOrEmpty(environment) || !string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase);
    }
}