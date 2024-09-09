using Randevucum.Authentication.Orchestrator.Contracts.Common;

namespace Randevucum.Authentication.Orchestrator.Contracts.Responses;

public record AuthResponse(OperationResult OperationResult, string AccessToken, string RefreshToken);
