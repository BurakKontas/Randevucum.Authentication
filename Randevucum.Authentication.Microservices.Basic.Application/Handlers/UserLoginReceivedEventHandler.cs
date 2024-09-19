using MassTransit;
using Microsoft.EntityFrameworkCore;
using Randevucum.Authentication.Common.Basic.Login;
using Randevucum.Authentication.Common.Basic.Register;
using Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;
using Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence;

namespace Randevucum.Authentication.Microservices.Basic.Application.Handlers;

public class UserLoginReceivedEventHandler(IBus bus, ReadDbContext readDbContext, WriteDbContext writeDbContext) : IConsumer<UserLoginReceivedEvent>
{
    private readonly IBus _bus = bus;
    private readonly ReadDbContext _readDbContext = readDbContext;
    private readonly WriteDbContext _writeDbContext = writeDbContext;

    public async Task Consume(ConsumeContext<UserLoginReceivedEvent> context)
    {
        //check if user exists
        var authProvider = context.Message.AuthProvider.ToAuthProviderList();
        var user = await _readDbContext.Users
            .Where(x => x.Email.Value == context.Message.Email)
            .Select(u => new
            {
                User = u,
                AuthProvider = u.AuthProviders.FirstOrDefault(ap => ap.ProviderName == authProvider)
            })
            .FirstOrDefaultAsync(result => result.AuthProvider != null, cancellationToken: context.CancellationToken);


        if (user is null)
        {
            await context.RespondAsync(new UserLoginFailedEvent("User doesn't exists"));
            return;
        }

        var userAggregate = new UserAggregate(user.User, new IpAddress(context.Message.IpAddress), new UserAgent(context.Message.UserAgent));

        //login
        var token = userAggregate.Login(context.Message.Password);

        //check password
        if (token is null)
        {
            await context.RespondAsync(new UserLoginFailedEvent("Password is incorrect"));
            return;
        }

        //save token
        await _writeDbContext.SaveChangesAsync(context.CancellationToken);

        await context.RespondAsync(new UserLoginSuccessEvent(token.Token, token.RefreshToken.Token));
    }
}