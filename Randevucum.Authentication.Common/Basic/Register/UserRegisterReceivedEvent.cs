﻿namespace Randevucum.Authentication.Common.Basic.Register;

public record UserRegisterReceivedEvent(
    string Email, 
    string Password, 
    string AuthProvider, 
    string IpAddress,
    string UserAgent,
    string? Phone = null, 
    string? ProviderUserId = null,
    bool IsEmailVerified = false, 
    bool IsPhoneVerified = false
);