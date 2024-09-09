namespace Randevucum.Authentication.Orchestrator.Contracts.Requests;

public record RegisterRequest(string Email, string Password, string Username, string Role);
