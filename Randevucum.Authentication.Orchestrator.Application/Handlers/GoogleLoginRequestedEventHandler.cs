using MassTransit;
using Randevucum.Authentication.Common.OAuth.GoogleLogin;

namespace Randevucum.Authentication.Orchestrator.Application.Handlers;

public class GoogleLoginRequestedEventHandler(IBus bus) : IConsumer<GoogleLoginRequestedEvent>
{
    private readonly IBus _bus = bus;

    public async Task Consume(ConsumeContext<GoogleLoginRequestedEvent> context)
    {
        var requestEvent = new GoogleLoginReceivedEvent(context.Message.AuthenticationCode);
        var response = await _bus.Request<GoogleLoginReceivedEvent, GoogleLoginResultedEvent>(requestEvent);

        await context.RespondAsync(response.Message);
    }
}