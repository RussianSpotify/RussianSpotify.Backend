using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotift.API.Data.PostgreSQL;

public class SubscriptionDbContext : DbContext, IExternalSubscriptionDbContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
    {
    }

    private SubscriptionDbContext()
    {
    }
    
    public DbSet<ExternalSubscription> ExternalSubscriptions { get; set; }
}