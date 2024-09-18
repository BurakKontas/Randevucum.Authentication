using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence;

public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<AuthProvider> AuthProviders { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<BearerToken> BearerTokens { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }
    public DbSet<PasswordResetRequest> PasswordResetsRequests { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }
    public DbSet<PhoneConfirmation> PhoneConfirmations { get; set; }

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
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

}