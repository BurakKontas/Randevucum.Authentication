using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence;

public class WriteDbContext(DbContextOptions<WriteDbContext> options, IPublisher publisher) : DbContext(options)
{
    private readonly IPublisher _publisher = publisher;

    public DbSet<User> Users { get; set; }
    public DbSet<AuthProvider> AuthProviders { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<BearerToken> BearerTokens { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }
    public DbSet<PasswordResetRequest> PasswordResetsRequests { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }
    public DbSet<PhoneConfirmation> PhoneConfirmations { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}