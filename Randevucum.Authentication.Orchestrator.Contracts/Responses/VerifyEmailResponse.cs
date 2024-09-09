using Randevucum.Authentication.Orchestrator.Contracts.Common;

namespace Randevucum.Authentication.Orchestrator.Contracts.Responses;

public record VerifyEmailResponse(OperationResult OperationResult, AuthResponse AuthResponse);
