using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotift.API.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="Song"/>
/// </summary>
public class SongConfiguration : EntityTypeConfigurationBase<Song>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Song> builder)
    {
        builder.Property(p => p.SongName)
            .HasComment("Название песни")
            .IsRequired();

        builder.Property(p => p.Duration)
            .HasComment("Длительность")
            .IsRequired();
        
        builder.Property(x => x.PlaysNumber)
            .HasComment("Кол-во прослушиваний")
            .HasDefaultValue(0);
        
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.HasMany(x => x.Playlists)
            .WithMany(y => y.Songs);

        builder.HasMany(x => x.Authors)
            .WithMany(y => y.Songs);

        builder.HasMany(x => x.Files)
            .WithOne(y => y.Song);

        builder.HasOne(i => i.Image)
            .WithMany()
            .HasForeignKey(i => i.ImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}