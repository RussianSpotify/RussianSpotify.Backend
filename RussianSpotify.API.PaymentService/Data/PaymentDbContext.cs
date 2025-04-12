using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.PaymentService.Domain.Entities;

namespace RussianSpotify.API.PaymentService.Data;

public class PaymentDbContext : DbContext, IDbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
    }
    
    private PaymentDbContext()
    {
    }
    
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}