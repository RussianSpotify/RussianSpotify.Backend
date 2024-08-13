using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotift.API.Data.PostgreSQL.Extensions;
using File = RussianSpotify.API.Core.Entities.File;

namespace RussianSpotift.API.Data.PostgreSQL.Confugurations;

/// <summary>
/// Конфигурация для <see cref="File"/>
/// </summary>
public class FileConfiguration : EntityTypeConfigurationBase<File>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<File> builder)
    {
        builder.Property(p => p.FileName)
            .HasComment("Название файла");

        builder.Property(p => p.ContentType)
            .HasComment("Тип файла");
        
        builder.ConfigureSoftDeletableEntity();
        builder.ConfigureTimeTrackableEntity();

        builder.Property(p => p.Address)
            .HasComment("Адрес файла в S3")
            .IsRequired();

        builder.Property(p => p.Size)
            .HasComment("Размер файла")
            .IsRequired();

        builder.HasOne(x => x.Song)
            .WithMany(y => y.Files)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Playlist)
            .WithOne(y => y.Image);

        builder.HasOne(i => i.User)
            .WithMany(i => i.Files)
            .IsRequired(false);
    }
}