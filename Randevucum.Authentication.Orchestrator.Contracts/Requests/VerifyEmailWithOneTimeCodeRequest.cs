namespace Randevucum.Authentication.Orchestrator.Contracts.Requests;

public record VerifyEmailWithOneTimeCodeRequest(string Code, string VerificationCode);
