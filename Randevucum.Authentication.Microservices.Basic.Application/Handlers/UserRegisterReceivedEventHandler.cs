using MassTransit;
using Randevucum.Authentication.Common.Basic.Register;

namespace Randevucum.Authentication.Microservices.Basic.Application.Handlers;

public class UserRegisterReceivedEventHandler(IBus bus) : IConsumer<UserRegisterReceivedEvent>
{
    private readonly IBus _bus = bus;

    public async Task Consume(ConsumeContext<UserRegisterReceivedEvent> context)
    {
        
    }
}