using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotift.API.Data.PostgreSQL.Extensions;

/// <summary>
/// Методы расширения для конфигурации сущностей
/// </summary>
public static class PropertyBuilderExtensions
{
    private const string NowCommand = "now()";
    
    /// <summary>
    /// Сконфигурировать сущность для обновления/создания
    /// </summary>
    /// <param name="builder">Конфигуратор</param>
    public static void ConfigureTimeTrackableEntity<TTimeTrackableEntity>(this EntityTypeBuilder<TTimeTrackableEntity> builder)
        where TTimeTrackableEntity : class, ITimeTrackable
    {
        builder.Property(p => p.CreatedAt)
            .HasComment("Дата создания")
            .HasDefaultValueSql(NowCommand);

        builder.Property(p => p.UpdatedAt)
            .HasComment("Дата обновления");
    }
    
    /// <summary>
    /// Сконфигурировать сущность, которая является SoftDeletable
    /// </summary>
    /// <param name="builder">Конфигуратор</param>
    public static void ConfigureSoftDeletableEntity<TSoftDeletedEntity>(this EntityTypeBuilder<TSoftDeletedEntity> builder)
        where TSoftDeletedEntity : class, ISoftDeletable
    {
        builder.Property(p => p.IsDeleted)
            .HasComment("Удален");

        builder.Property(p => p.DeletedAt)
            .HasComment("Дата удаления");
    }
}