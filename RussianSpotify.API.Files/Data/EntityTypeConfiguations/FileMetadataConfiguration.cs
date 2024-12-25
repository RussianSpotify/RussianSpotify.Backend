using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Shared.Data.PostgreSQL.EntityTypeConfiguration;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;

namespace RussianSpotify.API.Files.Data.EntityTypeConfiguations;

public class FileMetadataConfiguration : EntityTypeConfigurationBase<FileMetadata>
{
    protected override void ConfigureChild(EntityTypeBuilder<FileMetadata> builder)
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
    }
}