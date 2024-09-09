namespace Randevucum.Authentication.Orchestrator.Contracts.Requests;

public record UpdateUserRequest(string Token, string Username, string Email, string Role);
