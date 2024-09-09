using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Randevucum.Authentication.Orchestrator.Domain.Interfaces;
using Randevucum.Authentication.Orchestrator.Domain.Models;

namespace Randevucum.Authentication.Orchestrator.Application.Strategies;

public class HttpSecurityStrategy(IConfiguration configuration) : ISecurityStrategy
{
    private readonly IConfiguration _configuration = configuration;

    public OperationSafeResult CheckSecurityAsync(HttpContext context)
    {
        var salt = _configuration["TraceIdentifierSalt"];
        if (string.IsNullOrEmpty(salt))
        {
            throw new ArgumentNullException(nameof(salt), "TraceIdentifierSalt is not set in appsettings.json");
        }

        if (!context.Request.Headers.TryGetValue("OcRequestId", out var trace) ||
            !context.Request.Headers.TryGetValue("X-Trace-Hash", out var traceHash))
        {
            return new OperationSafeResult(false, StatusCodes.Status400BadRequest, "OcRequestId or X-Trace-Hash missing.");
        }

        var computedHash = CalculateSha256WithSalt(trace!, salt);
        if (!computedHash.Equals(traceHash))
            return new OperationSafeResult(false, StatusCodes.Status400BadRequest, "X-Trace-Hash is invalid.");

        return new OperationSafeResult(true, StatusCodes.Status200OK);
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
}