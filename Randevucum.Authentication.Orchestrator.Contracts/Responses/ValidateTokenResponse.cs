using Randevucum.Authentication.Orchestrator.Contracts.Common;

namespace Randevucum.Authentication.Orchestrator.Contracts.Responses;

public record ValidateTokenResponse(OperationResult OperationResult, string UserId);
