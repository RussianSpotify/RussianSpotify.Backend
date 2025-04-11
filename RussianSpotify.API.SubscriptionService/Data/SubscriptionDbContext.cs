using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.Grpc.SubscriptionService.Domain.Entities;

namespace RussianSpotify.Grpc.SubscriptionService.Data;

public class SubscriptionDbContext : DbContext, IDbContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options)
    {
    }
    
    private SubscriptionDbContext()
    {
    }

    public DbSet<Subscription> Subscriptions { get; set; }
    
    public DbSet<MessageOutbox> MessageOutboxes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}