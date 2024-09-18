using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Randevucum.Authentication.Microservices.Basic.Domain.Entities;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure.Persistence;

public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options)
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => throw new ApplicationException("Read context cannot write any data!");
}