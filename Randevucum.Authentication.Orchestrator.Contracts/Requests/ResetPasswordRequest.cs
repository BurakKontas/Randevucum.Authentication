namespace Randevucum.Authentication.Orchestrator.Contracts.Requests;

public record ResetPasswordRequest(string ResetToken, string NewPassword);
