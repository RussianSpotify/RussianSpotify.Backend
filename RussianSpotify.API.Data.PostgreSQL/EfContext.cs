using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using File = RussianSpotify.API.Core.Entities.File;

namespace RussianSpotift.API.Data.PostgreSQL;

/// <summary>
/// Контекст БД
/// </summary>
public class EfContext : DbContext, IDbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public EfContext(DbContextOptions<EfContext> options)
        : base(options)
    {
    }

    public EfContext()
    {
    }
    
    /// <inheritdoc />
    public DbSet<User> Users { get; set; }

    /// <inheritdoc />
    public DbSet<Role> Roles { get; set; }

    /// <inheritdoc />
    public DbSet<RolePrivilege> Privileges { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<Playlist> Playlists { get; set; } = default!;

    /// <inheritdoc /> 
    public DbSet<Song> Songs { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<Subscribe> Subscribes { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<Category> Categories { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<File> Files { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<Bucket> Buckets { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<EmailNotification> EmailNotifications { get; set; }

    /// <inheritdoc />
    public DbSet<Chat> Chats { get; set; }
    
    /// <inheritdoc />
    public DbSet<Message> Messages { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureGlobalFilters(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(Entry).Assembly);
    }

    private static void ConfigureGlobalFilters(ModelBuilder builder)
    {
        builder.Entity<Bucket>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<EmailNotification>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<File>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Playlist>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Song>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Subscribe>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
    }
}