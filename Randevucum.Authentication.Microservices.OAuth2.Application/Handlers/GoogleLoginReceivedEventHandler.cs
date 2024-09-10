using MassTransit;
using Randevucum.Authentication.Common.GoogleLogin;

namespace Randevucum.Authentication.Microservices.OAuth2.Application.Handlers;

public class GoogleLoginReceivedEventHandler : IConsumer<GoogleLoginReceivedEvent>
{
    public async Task Consume(ConsumeContext<GoogleLoginReceivedEvent> context)
    {
        var response = new GoogleLoginResultedEvent(true, "token", "refreshToken");

        await context.RespondAsync(response);
    }
}