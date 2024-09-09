namespace Randevucum.Authentication.Orchestrator.Contracts.Requests;

public record VerifyEmailWithCodeRequest(string Code, string VerificationCode);
