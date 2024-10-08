﻿using MassTransit;
using Randevucum.Authentication.Common.OAuth.GoogleLogin;

namespace Randevucum.Authentication.Microservices.OAuth2.Application.Handlers;

public class GoogleLoginReceivedEventHandler : IConsumer<GoogleLoginReceivedEvent>
{
    public async Task Consume(ConsumeContext<GoogleLoginReceivedEvent> context)
    {
        var response = new GoogleLoginSuccessEvent("token", "refreshToken");
        //var response = new GoogleLoginFailedEvent("message");

        await context.RespondAsync(response);
    }
}