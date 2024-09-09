namespace Randevucum.Authentication.Orchestrator.Contracts.Requests;

public record ChangePasswordRequest(string Token, string CurrentPassword, string NewPassword);
