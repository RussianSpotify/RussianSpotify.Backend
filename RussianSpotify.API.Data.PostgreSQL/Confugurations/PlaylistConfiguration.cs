#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

#endregion

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
///     Конфигурация для <see cref="Playlist" />
/// </summary>
public class PlaylistConfiguration : EntityTypeConfigurationBase<Playlist>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Playlist> builder)
    {
        builder.Property(x => x.PlaylistName)
            .HasComment("Название плейлиста")
            .IsRequired();

        builder.Property(p => p.ReleaseDate)
            .HasComment("Дата релиза");

        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.HasOne(x => x.Author)
            .WithMany(y => y.AuthorPlaylists)
            .HasForeignKey(x => x.AuthorId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Songs)
            .WithMany(y => y.Playlists);

        builder.HasMany(i => i.Users)
            .WithMany(i => i.Playlists);

        builder.Property(x => x.PlaysNumber).HasDefaultValue(0);
    }
}