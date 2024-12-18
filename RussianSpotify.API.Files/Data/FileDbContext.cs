using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Files.Data.EntityTypeConfiguations;
using RussianSpotify.API.Files.Domain.Entities;

namespace RussianSpotify.API.Files.Data;

public class FileDbContext : DbContext, IDbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public FileDbContext(DbContextOptions<FileDbContext> options)
        : base(options)
    {
    }

    public FileDbContext()
    {
    }
    
    public DbSet<FileMetadata> FilesMetadata { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FileMetadataConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}