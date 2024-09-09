namespace Randevucum.Authentication.Orchestrator.Domain.Models;

public record OperationSafeResult(bool IsSafe, int StatusCode, string? Message = null);