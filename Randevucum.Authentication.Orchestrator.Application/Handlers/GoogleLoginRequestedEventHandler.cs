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

        // burası googleden veriyi alıp sonra Basic servisine göndericek ve oradan gelen veriyi dönecek dönüş tipide bu handlerin yeni bir event olacak AuthenticationResultedEvent olacak
        // sonuçta yapmaya çalıştığımız şey Google'den veriyi alıp Basic ile aynı şekilde oluşturup yeni basic tokeni döndürmek...

        await context.RespondAsync(response.Message);
    }
}