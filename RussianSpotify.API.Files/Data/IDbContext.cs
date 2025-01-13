#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RussianSpotify.API.Files.Domain.Entities;

#endregion

namespace RussianSpotify.API.Files.Data;

/// <summary>
///     Интерфейс контекста бд
/// </summary>
public interface IDbContext
{
    /// <summary>
    ///     Коллекция метаданных файлов.
    /// </summary>
    DbSet<FileMetadata> FilesMetadata { get; set; }

    /// <summary>
    ///     Сохранить изменения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Кол-во затронутых записей</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Фасад базы
    /// </summary>
    public DatabaseFacade Database { get; }
}