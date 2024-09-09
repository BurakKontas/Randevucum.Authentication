using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Randevucum.Authentication.Orchestrator.Domain.Interfaces;
using Randevucum.Authentication.Orchestrator.Domain.Models;

namespace Randevucum.Authentication.Orchestrator.Application.Strategies;

public class Http2SecurityStrategy : ISecurityStrategy
{
    public OperationSafeResult CheckSecurityAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Hash", out _))
            return new OperationSafeResult(false, StatusCodes.Status400BadRequest, "X-Hash header is missing.");

        return new OperationSafeResult(true, StatusCodes.Status200OK);
    }
}