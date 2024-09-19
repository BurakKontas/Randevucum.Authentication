using MassTransit;
using Microsoft.EntityFrameworkCore;
using Randevucum.Authentication.Common.Basic.Register;
using Randevucum.Authentication.Microservices.Basic.Domain.Aggregates;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Enums.Extensions;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;
using Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence;

namespace Randevucum.Authentication.Microservices.Basic.Application.Handlers;

public class UserRegisterReceivedEventHandler(IBus bus, ReadDbContext readDbContext, WriteDbContext writeDbContext) : IConsumer<UserRegisterReceivedEvent>
{
    private readonly IBus _bus = bus;
    private readonly ReadDbContext _readDbContext = readDbContext;
    private readonly WriteDbContext _writeDbContext = writeDbContext;

    public async Task Consume(ConsumeContext<UserRegisterReceivedEvent> context)
    {
        //check if user exists
        var authProvider = context.Message.AuthProvider.ToAuthProviderList();
        var userExists = await _readDbContext.Users
            .Where(x => x.Email.Value == context.Message.Email)
            .SelectMany(x => x.AuthProviders)
            .AnyAsync(ap => ap.ProviderName == authProvider, cancellationToken: context.CancellationToken);

        if (userExists)
        {
            await context.RespondAsync(new UserRegisterFailedEvent("User already exists"));
            return;
        }

        //create user
        var userId = new UserId(Guid.NewGuid());
        var email = new Email(context.Message.Email);
        var password = new Password(context.Message.Password);
        var phone = context.Message.Phone is not null ? new Phone(context.Message.Phone) : null;
        var isEmailVerified = context.Message.IsEmailVerified;
        var isPhoneVerified = context.Message.IsPhoneVerified;

        var user = User.Create(userId, email, password, isEmailVerified, DateTime.UtcNow, DateTime.Now, phone,
            isPhoneVerified);

        //add auth provider
        var userAggregate = new UserAggregate(user, new IpAddress(context.Message.IpAddress), new UserAgent(context.Message.UserAgent));

        var providerUserId = userId.Value.ToString();
        if(context.Message.ProviderUserId is not null)
            providerUserId = context.Message.ProviderUserId;

        userAggregate.AddAuthProvider(authProvider, providerUserId);

        //save user
        await _writeDbContext.Users.AddAsync(user, cancellationToken: context.CancellationToken);

        await _writeDbContext.SaveChangesAsync(context.CancellationToken);

        await context.RespondAsync(new UserRegisterSuccessEvent(user.Id.Value));
    }
}