using MassTransit;
using Randevucum.Authentication.Common.Basic.Login;

namespace Randevucum.Authentication.Microservices.Basic.Application.Handlers;

public class UserLoginReceivedEventHandler(IBus bus) : IConsumer<UserLoginReceivedEvent>
{
    private readonly IBus _bus = bus;

    public async Task Consume(ConsumeContext<UserLoginReceivedEvent> context)
    {
        
    }
}