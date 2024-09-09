using Randevucum.Authentication.Orchestrator.Contracts.Common;

namespace Randevucum.Authentication.Orchestrator.Contracts.Responses;

public record RegisterResponse(OperationResult OperationResult, string UserId, string Username, string Email);
