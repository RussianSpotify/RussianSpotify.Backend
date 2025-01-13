#region

using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Files.Data.EntityTypeConfigurations;
using RussianSpotify.API.Files.Domain.Entities;

#endregion

namespace RussianSpotify.API.Files.Data;

/// <summary>
///     Контекст базы данных для работы с файлами и их метаданными.
/// </summary>
public class FileDbContext : DbContext, IDbContext
{
    /// <summary>
    ///     Конструктор контекста данных с настройками.
    /// </summary>
    /// <param name="options">Настройки контекста данных.</param>
    public FileDbContext(DbContextOptions<FileDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    ///     Конструктор контекста данных по умолчанию.
    /// </summary>
    public FileDbContext()
    {
    }

    /// <summary>
    ///     Коллекция метаданных файлов.
    /// </summary>
    public DbSet<FileMetadata> FilesMetadata { get; set; }

    /// <summary>
    ///     Конфигурирует модели данных при их создании.
    /// </summary>
    /// <param name="modelBuilder">Строитель модели данных.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Применение конфигурации для метаданных файлов
        modelBuilder.ApplyConfiguration(new FileMetadataConfiguration());

        // Вызов базового метода для выполнения конфигурации по умолчанию
        base.OnModelCreating(modelBuilder);
    }
}