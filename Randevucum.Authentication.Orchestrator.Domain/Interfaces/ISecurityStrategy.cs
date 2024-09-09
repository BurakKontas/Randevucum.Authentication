using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Randevucum.Authentication.Orchestrator.Domain.Models;

namespace Randevucum.Authentication.Orchestrator.Domain.Interfaces;

public interface ISecurityStrategy
{
    OperationSafeResult CheckSecurityAsync(HttpContext context);
}