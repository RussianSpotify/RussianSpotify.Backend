using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RussianSpotift.API.Data.PostgreSQL.Confugurations;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using File = RussianSpotify.API.Core.Entities.File;

namespace RussianSpotift.API.Data.PostgreSQL;

/// <summary>
/// Контекст БД
/// </summary>
public class EfContext
    : IdentityDbContext<User, Role, Guid>, IDbContext
{
    /// <summary>
    /// Пустой конструктор
    /// </summary>
    public EfContext()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public EfContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc cref="IdentityDbContext{TUser,TRole,TKey,TUserClaim,TUserRole,TUserLogin,TRoleClaim,TUserToken}.Roles" />
    public override DbSet<Role> Roles { get; set; } = default!;

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
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Entry).Assembly);
        base.OnModelCreating(builder);
    }
}